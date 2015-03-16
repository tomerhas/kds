package automationFramework;

import java.io.File;
import java.util.concurrent.TimeUnit;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.ie.InternetExplorerDriver;
import org.openqa.selenium.support.ui.Select;

import pageObjects.Employee_Card;
import pageObjects.LogIn_Page;

public abstract class Base {

	public static WebDriver Initialize_browser( ) {
		
		File file = new File("C:/Selenium/IEDriverServer.exe");
		System.setProperty("webdriver.ie.driver", file.getAbsolutePath());
		return  new InternetExplorerDriver();
		 
		 	
	}
	
    
   public static  void  Initialize_Webpage(WebDriver driver) {
	   
	   
	   driver.navigate().to("http://kdstest");
	   
	  
		 	
	}
	  
	
   
   
 public static  void  Enter_Workcard(WebDriver driver) {
	   
	   
	 LogIn_Page.lnk_EmployeeCards(driver).click();
	  driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	  Select droplist = new Select(Employee_Card.List_Month(driver));
     droplist.selectByVisibleText("03/2015"); 
     Employee_Card.Btn_Execute(driver).click();
     Employee_Card.Link_Date(driver).click();
	   
	  
		 	
	}
   
   
	
	
	
}
