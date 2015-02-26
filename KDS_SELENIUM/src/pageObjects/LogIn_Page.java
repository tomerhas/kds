package pageObjects;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

public class LogIn_Page {

	 private static WebElement element;
	 
	 public static WebElement lnk_EmployeeCards(WebDriver driver){
		 
		    element = driver.findElement(By.id("ctl00_KdsContent_btnUpdWorkCard"));
		 
		    return element;
		 
		    }
	
	
	

}
