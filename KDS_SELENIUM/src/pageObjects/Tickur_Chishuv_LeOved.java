package pageObjects;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;





public class Tickur_Chishuv_LeOved {
	
	
	
 private static WebElement element;
	 
	 public static WebElement Mispar_Ishi(WebDriver driver){
		 
		    element = driver.findElement(By.id("ctl00_KdsContent_txtEmpId"));
		 
		    return element;
		 
		    }
	
	 
	 
	 
	
	 public static WebElement Btn_Show(WebDriver driver){
		 
		    element = driver.findElement(By.id("ctl00_KdsContent_btnShow"));
		 
		    return element;
		 
		    }
	 
	 
	 
	 
	 

}
