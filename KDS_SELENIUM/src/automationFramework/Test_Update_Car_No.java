package automationFramework;

import org.openqa.selenium.WebDriver;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.AfterMethod;

import pageObjects.Work_Card;

public class Test_Update_Car_No {
	
	public WebDriver driver;	
	
	
  @Test
  public void f() {
	  
	  
	  Work_Card.Car_Num(driver).sendKeys("34918");
	  
	  
	  
	  
	  
	  
  }
  
  
  
  
  
  
  
  
  
  @BeforeMethod
  public void beforeMethod() {
	  
	  
	  driver=Base.Initialize_browser();
	  Base.Initialize_Webpage(driver);
	  Base.Enter_Workcard(driver);
	  
	  
  }

  
  
  
  
  
  
  
  
  
  
  
  
  
  @AfterMethod
  public void afterMethod() {
  }
   
}
