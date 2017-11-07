package pageObjects;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

public class LogInPage {

	 private static WebElement element;
	 
	 public static WebElement lnk_EmployeeCards(WebDriver driver){
		 
		    element = driver.findElement(By.id("ctl00_KdsContent_btnUpdWorkCard"));
		 
		    return element;
		 
		    }
	 
	 
	 
	 


	 
	 public static WebElement lnk_Home_Page(WebDriver driver){
		 
		    element = driver.findElement(By.id("ctl00_ImageHome"));
		 
		    return element;
		 
		    }
	 
	 
	 

	 
	 public static WebElement Txt_Change_User(WebDriver driver){
		 
		    element = driver.findElement(By.id("ctl00_KdsContent_txtImpersonate"));
		 
		    return element;
		 
		    }
	 
	 
	 
	 

	 
	 public static WebElement Btn_Change_User(WebDriver driver){
		 
		    element = driver.findElement(By.id("ctl00_KdsContent_btnImpersonate"));
		 
		    return element;
		 
		    }
	 
	 
	 
	
	
	

}
