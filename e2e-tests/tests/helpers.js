const { expect } = require('@playwright/test');

const urls = {
  base: 'https://localhost:44364'
}

const user = {
  email: 'alex@andrei',
  password: 'Copernic@1234',
  name: 'Alex'
}

const registerUser = {
  email: 'register1@email.com',  //must be changed for each test
  password: 'Registertest1!',
  confirmPassword: 'Registertest1!',
  firstName: 'Test',
  lastName: 'Register',
  birthDay: '2000-01-01',
}

async function loginUser(page, email=user.email, password=user.password) {
  await page.goto(`${urls.base}/CamperAccount/Login`);

  await page.fill('#Email', email);
  await page.fill('#Password', password);
  await page.click('#submitButton');

  await expect(page).toHaveURL(urls.base, { timeout: 5000 });
}

async function logoutUser(page) {
  await page.goto(urls.base);

  await page.locator('div.my-account').hover();
  await page.click('text="Logout"');

  await expect(page).toHaveURL(urls.base);
  await expect(page.getByText('Autentifica-te')).toBeVisible();
}

export { urls, user, registerUser, loginUser, logoutUser};