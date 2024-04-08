const { test, expect } = require('@playwright/test');

test.describe('Login functionality', () => {
  test('Successful login redirects to homepage', async ({ page }) => {
    await page.goto('https://localhost:44364/CamperAccount/Login');

    await page.fill('#Email', 'alex@andrei');
    await page.fill('#Password', 'Copernic@1234');

    await page.click('#submitButton');

    await expect(page).toHaveURL('https://localhost:44364/');

    const accountLink = page.locator('div.my-account');
    await expect(accountLink).toBeVisible();
  });
});