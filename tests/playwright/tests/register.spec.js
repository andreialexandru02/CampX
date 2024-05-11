const { test, expect } = require('@playwright/test');
const { registerUser, urls } = require('./helpers');

test.describe('Register functionality', () => {
    test.beforeEach(async ({
        page
    }) => {
        await page.goto(`${urls.base}/CamperAccount/Register`);
        await expect(page).toHaveTitle(/View - CampX/);
    });

    test('Successful registration redirects to homepage', async ({
        page
    }) => {
        await page.fill('#Email', registerUser.email);
        await page.fill('#Password', registerUser.password);
        await page.fill('#ConfirmPassword', registerUser.confirmPassword);
        await page.fill('#FirstName', registerUser.firstName);
        await page.fill('#LastName', registerUser.lastName);
        await page.fill('#BirthDay', registerUser.birthDay);
        await page.click('#submitButton');
        await page.waitForTimeout(5000);
        await expect(page).toHaveURL(urls.base, { timeout: 5000 });
        const myAccountDiv = page.locator('div.my-account');
        await expect(myAccountDiv).toBeVisible();
        await expect(myAccountDiv).toContainText('Test');
    });

    test('Wrong email format shows an error message', async ({ page }) => {
        await page.fill('#Email', 'email.com');
        await page.fill('#Password', registerUser.password);
        await page.fill('#ConfirmPassword', registerUser.confirmPassword);
        await page.fill('#FirstName', registerUser.firstName);
        await page.fill('#LastName', registerUser.lastName);
        await page.fill('#BirthDay', registerUser.birthDay);
        await page.click('#submitButton');

        await expect(page.getByText('Formatul email-ului nu este corect!')).toBeVisible();
    });

    test('Password too short shows an error message', async ({ page }) => {
        await page.fill('#Email', registerUser.email);
        await page.fill('#Password', 'short');
        await page.fill('#ConfirmPassword', 'short');
        await page.fill('#FirstName', registerUser.firstName);
        await page.fill('#LastName', registerUser.lastName);
        await page.fill('#BirthDay', registerUser.birthDay);
        await page.click('#submitButton');

        await expect(page.getByText('Parola trebuie sa aiba mai mult de 10 caractere')).toBeVisible();
    });

    test('Password does not match confirm password shows an error message', async ({ page }) => {
        await page.fill('#Email', registerUser.email);
        await page.fill('#Password', registerUser.password);
        await page.fill('#ConfirmPassword', 'NotMatching');
        await page.fill('#FirstName', registerUser.firstName);
        await page.fill('#LastName', registerUser.lastName);
        await page.fill('#BirthDay', registerUser.birthDay);
        await page.click('#submitButton');

        await expect(page.getByText('Cele 2 parole nu coincid!')).toBeVisible();
    });

    test('Displays error for missing credentials', async ({ page }) => {
        await page.click('#submitButton');

        await expect(page.getByText('The Email field is required.')).toBeVisible();
        await expect(page.getByText('The Password field is required.')).toBeVisible();
        await expect(page.getByText('The ConfirmPassword field is required.')).toBeVisible();
        await expect(page.getByText('The FirstName field is required.')).toBeVisible();
        await expect(page.getByText('The LastName field is required.')).toBeVisible();
        await expect(page.getByText('The BirthDay field is required.')).toBeVisible();
    });

    test('Displays error for existing email', async ({ page }) => {
        await page.fill('#Email', 'alex@andrei');
        await page.fill('#Password', registerUser.password);
        await page.fill('#ConfirmPassword', registerUser.confirmPassword);
        await page.fill('#FirstName', registerUser.firstName);
        await page.fill('#LastName', registerUser.lastName);
        await page.fill('#BirthDay', registerUser.birthDay);
        await page.click('#submitButton');

        await expect(page.getByText('Exista deja un utilizator cu acest email!')).toBeVisible();
    });
});
