import os
import pytest
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from dotenv import load_dotenv

load_dotenv()
chrome_driver_path = os.getenv('CHROME_DRIVER_PATH')
base_url = os.getenv('BASE_URL')

@pytest.fixture(scope="module")
def driver():
    service = webdriver.ChromeService(executable_path=chrome_driver_path)
    driver = webdriver.Chrome(service=service)
    yield driver
    driver.quit()

def login_user(driver, base_url, email, password):
    driver.get(f"{base_url}/CamperAccount/Login")
    driver.find_element(By.ID, "Email").send_keys(email)
    driver.find_element(By.ID, "Password").send_keys(password)
    driver.find_element(By.ID, "submitButton").click()

def logout_user(driver, base_url):
    driver.get(base_url)
    account_div = WebDriverWait(driver, 10).until(EC.presence_of_element_located((By.CSS_SELECTOR, 'div.my-account')))
    account_div.click()
    logout_button = WebDriverWait(driver, 10).until(EC.element_to_be_clickable((By.XPATH, "//button[contains(text(), 'Logout')]")))
    logout_button.click()

def test_successful_login(driver):
    login_user(driver, base_url, 'alex@andrei', 'Copernic@1234')
    WebDriverWait(driver, 10).until(EC.url_to_be(f"{base_url}/"))
    assert "Home Page" in driver.title

def test_unsuccessful_login(driver):
    login_user(driver, base_url, 'alex@andrei', 'WrongPassword')
    WebDriverWait(driver, 10).until(EC.url_to_be(f"{base_url}/CamperAccount/Login"))
    assert "Invalid credentials" in driver.page_source

def test_missing_credentials(driver):
    driver.get(f"{base_url}/CamperAccount/Login")
    driver.find_element(By.ID, "submitButton").click()
    assert "The Email field is required." in driver.page_source
    assert "The Password field is required." in driver.page_source


def test_logout(driver):
    login_user(driver, base_url, 'alex@andrei', 'Copernic@1234')
    WebDriverWait(driver, 10).until(EC.url_to_be(f"{base_url}/"))
    logout_user(driver, base_url)
    WebDriverWait(driver, 10).until(EC.url_to_be(f"{base_url}/"))
    assert "Home Page" in driver.title
