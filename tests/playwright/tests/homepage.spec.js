const { test, expect } = require('@playwright/test');

test.describe('HomePage functionality', () => {
  test('Successful redirect from homepage to login', async ({ page }) => {
    await page.goto('https://localhost:44364/');
    const accountLink = page.locator('div.my-account');
    const loginHref = page.locator('a[href="/CamperAccount/Login"]:has-text("Login")');
    
    await expect(accountLink).toBeVisible();
    await page.hover('div.my-account')

    await expect(loginHref).toBeVisible();
    await page.click('a[href="/CamperAccount/Login"]:has-text("Login")');

    await expect(page).toHaveURL('https://localhost:44364/CamperAccount/Login');
  });
  test('Successful redirect from homepage to register', async ({ page }) => {
    await page.goto('https://localhost:44364/');
    const accountLink = page.locator('div.my-account');
    
    const registerHref = page.locator('a[href="/CamperAccount/Register"]:has-text("Register")');
    await expect(accountLink).toBeVisible();
    await page.hover('div.my-account')

    await expect(registerHref).toBeVisible();
    await page.click('a[href="/CamperAccount/Register"]:has-text("Register")');

    await expect(page).toHaveURL('https://localhost:44364/CamperAccount/Register');
  });
  test('Successfull redirect to homepage', async ({ page }) => {
    await page.goto('https://localhost:44364/');
    const accountLink = page.locator('div.my-account');
    
    const logoHref = page.locator('a[href="/Home/Index"]');
    await expect(logoHref).toBeVisible();
    await page.click('a[href="/Home/Index"]')

    await expect(page).toHaveURL('https://localhost:44364/Home/Index');
  });
  test('Successfull redirect to Map', async ({ page }) => {
    await page.goto('https://localhost:44364/');
    const mapLink = page.locator('a[href="/Map/ShowMap"]');

    await expect(mapLink).toBeVisible();
    await page.click('a[href="/Map/ShowMap"]')

    await expect(page).toHaveURL('https://localhost:44364/Map/ShowMap');
  });
  test('Successfull redirect to Trips', async ({ page }) => {
    await page.goto('https://localhost:44364/');
    const tripLink = page.locator('a[href="/Trip/ShowTrips"]');

    await expect(tripLink).toBeVisible();
    await page.click('a[href="/Trip/ShowTrips"]')

    await expect(page).toHaveURL('https://localhost:44364/Trip/ShowTrips');
  });
});