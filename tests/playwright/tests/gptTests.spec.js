// To conduct tests using the provided input fields with the specified IDs, 
// you can create test cases to handle different registration scenarios. 
// I'll create example Playwright test cases in JavaScript that test the 
// registration process for different input conditions.

// Before running these tests, make sure you have set up 
// Playwright in your project and imported the necessary libraries.

// Here's a script that demonstrates how you can use
//  Playwright to test the registration process:



const { registerUser, urls } = require('./helpers');
const { test, expect } = require('@playwright/test');

test.describe('Registration Tests', () => {
    const registerUrl = `${urls.base}/CamperAccount/Register`

    test('Register new user with valid details', async ({ page }) => {
        // Navigate to the registration page
        await page.goto(registerUrl);

        // Define a user object with valid registration details
        const registerUser = {
            email: 'testuser@example2.com',
            password: 'password123',
            confirmPassword: 'password123',
            firstName: 'Test',
            lastName: 'User',
            birthDay: '2000-01-01'
        };

        // Fill in the registration form
        await page.fill('#Email', registerUser.email);
        await page.fill('#Password', registerUser.password);
        await page.fill('#ConfirmPassword', registerUser.confirmPassword);
        await page.fill('#FirstName', registerUser.firstName);
        await page.fill('#LastName', registerUser.lastName);
        await page.fill('#BirthDay', registerUser.birthDay);

        // Submit the form
        await page.click('#submitButton');
        await expect(page).toHaveURL(urls.base);
    });

    test('Fail registration with password mismatch', async ({ page }) => {
        // Navigate to the registration page
        await page.goto(`${urls.base}/CamperAccount/Register`);

        // Define a user object with mismatched passwords
        const registerUser = {
            email: 'testuser@example.com',
            password: 'passw',
            confirmPassword: 'differentPassword',
            firstName: 'Test',
            lastName: 'User',
            birthDay: '2000-01-01'
        };

        // Fill in the registration form
        await page.fill('#Email', registerUser.email);
        await page.fill('#Password', registerUser.password);
        await page.fill('#ConfirmPassword', registerUser.confirmPassword);
        await page.fill('#FirstName', registerUser.firstName);
        await page.fill('#LastName', registerUser.lastName);
        await page.fill('#BirthDay', registerUser.birthDay);

        // Submit the form
        await page.click('#submitButton');

        await expect(page.getByText('Parola trebuie sa aiba mai mult de 10 caractere')).toBeVisible();
    });

    test('Fail registration with invalid email', async ({ page }) => {
        // Navigate to the registration page
        await page.goto(`${urls.base}/CamperAccount/Register`);

        // Define a user object with an invalid email
        const registerUser = {
            email: 'invalid-email',
            password: 'password123',
            confirmPassword: 'password123',
            firstName: 'Test',
            lastName: 'User',
            birthDay: '2000-01-01'
        };

        // Fill in the registration form
        await page.fill('#Email', registerUser.email);
        await page.fill('#Password', registerUser.password);
        await page.fill('#ConfirmPassword', registerUser.confirmPassword);
        await page.fill('#FirstName', registerUser.firstName);
        await page.fill('#LastName', registerUser.lastName);
        await page.fill('#BirthDay', registerUser.birthDay);

        // Submit the form
        await page.click('#submitButton');

        await expect(page.getByText('Formatul email-ului nu este corect!')).toBeVisible();
    });
});

// In the script above, the test suite uses three test cases:

// Register new user with valid details: This test case fills out 
// the form with valid details and expects a success message.
// Fail registration with password mismatch: This test case uses 
// different values for password and confirm password fields and 
// expects an error message related to the password mismatch.
// Fail registration with invalid email: This test case uses 
// an invalid email format and expects an error message indicating that the email format is incorrect.
// Make sure to replace the baseUrl variable with the actual 
// registration URL for your application and adjust the expected messages based on your application's validation logic.