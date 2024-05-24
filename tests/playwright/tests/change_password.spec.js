const { test, expect } = require('@playwright/test');
const { urls, user, loginUser, logoutUser } = require('./helpers');
const { timeout } = require('../playwright.config');

async function changePassword(page, oldPassword, newPassword, confirmPassword) {
  await page.goto(urls.base);
  await page.locator('div.my-account').hover();
  await page.click('text="Profilul meu"');
  await page.click('text="Schimba Parola"');

  await page.fill('#OldPassword', oldPassword);
  await page.fill('#Password', newPassword);
  await page.fill('#ConfirmPassword', confirmPassword);
  await page.click('#submitButton');
}

const oldPassword = user.password;
const newPassword = 'Pa$$w0rd-007';

test.describe('Change password functionality', () => {
  
  test.beforeEach(async ({ page }) => {
    await loginUser(page);
  });

  test.skip('User can change password and log in with new credentials', async ({ page }) => {
    await changePassword(page, oldPassword, newPassword, newPassword);
    await expect(page).toHaveURL(urls.base);
    
    await logoutUser(page);
    await loginUser(page, user.email, newPassword);
    
    // Reset to the old password for cleanliness
    await changePassword(page, newPassword, oldPassword, oldPassword); 
    
    await expect(page).toHaveURL(urls.base, { timeout: 5000 });
  });

  test('Shows error when old and new passwords are not provided', async ({ page }) => {
    await changePassword(page, '', '', '');
    await expect(page.locator('text="The OldPassword field is required."')).toBeVisible();
    await expect(page.locator('text="The Password field is required."')).toBeVisible();
    await expect(page.locator('text="The ConfirmPassword field is required."')).toBeVisible();
  });

  test('Shows error for incorrect old password', async ({ page }) => {
    await changePassword(page, 'incorrectOldPassword', newPassword, newPassword);
    await expect(page.locator('text="Parola gresita!"')).toBeVisible();
  });

  test('Shows error for new password below length requirement', async ({ page }) => {
    await changePassword(page, oldPassword, 'short', 'short');
    await expect(page.locator('text="Parola trebuie sa aiba mai mult de 10 caractere"')).toBeVisible();
  });

  test('Shows error when new password and confirmation do not match', async ({ page }) => {
    await changePassword(page, oldPassword, newPassword, `${newPassword}-typo`);
    await expect(page.locator('text="Cele 2 parole nu coincid!"')).toBeVisible();
  });
});
