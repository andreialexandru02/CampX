const { test, expect } = require('@playwright/test');
const { urls, user, loginUser } = require('./helpers');

async function login(page, email, password) {
  await page.fill('#Email', email);
  await page.fill('#Password', password);
  await page.click('#submitButton');
}

test.describe('Login functionality', () => {
  test.beforeEach(async ({ page }) => {
    await page.goto(`${urls.base}/CamperAccount/Login`);

    await expect(page).toHaveTitle(/Login - CampX/);
  });

  test('Successful login redirects to homepage', async ({ page }) => {
    await login(page, user.email, user.password);

    await expect(page).toHaveURL(urls.base);

    const myAccountDiv = page.locator('div.my-account');
    await expect(myAccountDiv).toBeVisible();
    await expect(myAccountDiv).toContainText(user.name);
  });

  test('Unsuccessful login shows an error message', async ({ page }) => {
    await login(page, 'alex@andrei', 'IncorrectPass');

    await expect(page.getByText('Invalid credentials')).toBeVisible();
  });

  test('Displays error for missing credentials', async ({ page }) => {
    await page.click('#submitButton');

    await expect(page.getByText('The Email field is required.')).toBeVisible();
    await expect(page.getByText('The Password field is required.')).toBeVisible();
  });
});

test.describe('Logout functionality', () => {
  
  test.beforeEach(async ({ page }) => {
    await loginUser(page);
  });

  test('User can logout successfully', async ({ page }) => {
    await page.locator('div.my-account').hover();

    await page.click('text="Logout"');

    await expect(page).toHaveURL(urls.base, { timeout: 5000 });
    await expect(page).toHaveTitle(/Home Page - CampX/);
    await expect(page.getByText('Autentifica-te')).toBeVisible();
  });
});