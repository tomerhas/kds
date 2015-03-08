package automationFramework;

import java.io.File;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.ie.InternetExplorerDriver;
import org.openqa.selenium.support.ui.Select;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.AfterMethod;

import pageObjects.Employee_Card;
import pageObjects.LogIn_Page;




public class Test_Work_Card {
	public WebDriver driver;
  @Test
  
  
  
 public void f() {
	  
	  
	  
	  
	  
  }
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  @BeforeMethod
  public void beforeMethod() {
	  
	  File file = new File("C:/Selenium/IEDriverServer.exe");
	  System.setProperty("webdriver.ie.driver", file.getAbsolutePath());
	  driver = new InternetExplorerDriver();
	  driver.navigate().to("http://kdstest");
	  LogIn_Page.lnk_EmployeeCards(driver).click();
	  Select droplist = new Select(Employee_Card.List_Month(driver));
      droplist.selectByVisibleText("03/2015"); 
      Employee_Card.Btn_Execute(driver).click();
      Employee_Card.Link_Date(driver).click();
	  
  }

  @AfterMethod
  public void afterMethod() {
  }

}
