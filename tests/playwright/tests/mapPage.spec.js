const { test, expect } = require('@playwright/test');

test.describe('MapPage functionality', () => {
  test('Successful create click', async ({ page }) => {
    
    await page.goto('https://localhost:44364/Map/ShowMap');

    await expect(page.locator('#addCampsite')).toBeVisible();

    await page.click('#addCampsite')

    const isGuest = await page.isVisible('div.my-account:has-text("Autentifica-te")');
    if(isGuest){
        await expect(page).toHaveURL('https://localhost:44364/CamperAccount/Login?ReturnUrl=%2FMap%2FAddCampsite');
    }
    else{
        await expect(page).toHaveURL('https://localhost:44364/Map/AddCampsite');
    }
  });
  test('Successful details click', async ({ page }) => {
    
    await page.goto('https://localhost:44364/Map/ShowMap');

    await expect(page.locator('table.table')).toBeVisible();

    const isGuest = await page.isVisible('div.my-account:has-text("Autentifica-te")');

    const campsiteRows = await page.$$('table.table tbody tr');
    const randomRowIndex = Math.floor(Math.random() * campsiteRows.length);

    const randomRow = campsiteRows[randomRowIndex];
    const link = await randomRow.$('a');
    const href = await link.getAttribute('href');
    const campsiteId = href.split('/')[href.split('/').length-1]; 
    await link.click();
    
    if(isGuest){
        await expect(page).toHaveURL(`https://localhost:44364/CamperAccount/Login?ReturnUrl=%2FMap%2FCampsiteDetails%2F${campsiteId}`);
    }
    else{
        await expect(page).toHaveURL(`https://localhost:44364/Map/CampsiteDetails/${campsiteId}`);
    }
  });
});