# Setting Up Selenium with Unittest

## Prerequisites

- Python installed on your system.
- Chrome WebDriver installed. You can download it from [here](https://googlechromelabs.github.io/chrome-for-testing/#stable).
- Set up a virtual environment and activate it:

```
./venv/Scripts/activate
```

## Installation

Install the required Python packages using pip:

```
pip install -r requirements.txt
```

## Chrome WebDriver Setup

If you're using Windows, you can skip this step as Chrome WebDriver is included in the repository.

For other operating systems, download the Chrome WebDriver from the link provided above and add the path to the WebDriver in the .env file.

Example .env file:

```
CHROME_DRIVER_PATH=/path/to/chromedriver
```

## Running Tests

Once you have set up everything, you can run the tests using the following command:

```
pytest test.py
```

This command will execute the tests defined in test.py.