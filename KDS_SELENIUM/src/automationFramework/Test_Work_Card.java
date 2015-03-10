package automationFramework;

import java.io.File;
import java.util.concurrent.TimeUnit;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.ie.InternetExplorerDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.AfterMethod;

import pageObjects.Employee_Card;
import pageObjects.LogIn_Page;
import pageObjects.Work_Card;

import java.lang.Thread;




public class Test_Work_Card {

	public WebDriver driver;
	public WebElement element;
	
	 @DataProvider(name = "Param_dd")
	  public Object[][] Parameters_dd() {
	          return new Object[][] {
	            { "��� ������","���� ���"},
	        	{ "���� ������","��� ����"}
	           
	           };
	                    
	          };
	
	
	
	
	
	
  @Test ( dataProvider = "Param_dd")
  
	
  
 public void f(String sTachograph,String sLina ) throws InterruptedException {




      for (String handle : driver.getWindowHandles()) {
      driver.switchTo().window(handle);}
	  driver.manage().timeouts().implicitlyWait(30, TimeUnit.SECONDS);
	  WebDriverWait wait = new WebDriverWait(driver, 10);
	  wait.until(ExpectedConditions.textToBePresentInElementValue(Work_Card.TxtId(driver), "31777"));
	  Work_Card.TxtId(driver).sendKeys("77104");
	  Work_Card.Date(driver).sendKeys("03/03/2015");
	  Work_Card.Btn_Show(driver).click();
	  Thread.sleep(3000);
	  Work_Card.Day_Plus(driver).click();
	  Select droplist = new Select(Work_Card.Tachograph(driver));
      droplist.selectByVisibleText(sTachograph); 
      Select droplist1 = new Select(Work_Card.Lina(driver));
      droplist1.selectByVisibleText(sLina); 
      Work_Card.Btn_Update(driver).click();
      Work_Card.Btn_Close(driver).click();
	  
	  
  }
  
  
  
  

  
  
  
  
  
  
  
  
  

















@BeforeMethod
  public void beforeMethod() {
	  
	  File file = new File("C:/Selenium/IEDriverServer.exe");
	  System.setProperty("webdriver.ie.driver", file.getAbsolutePath());
	  driver = new InternetExplorerDriver();
	  driver.navigate().to("http://kdstest");
	  LogIn_Page.lnk_EmployeeCards(driver).click();
	  driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	  Select droplist = new Select(Employee_Card.List_Month(driver));
      droplist.selectByVisibleText("03/2015"); 
      Employee_Card.Btn_Execute(driver).click();
      Employee_Card.Link_Date(driver).click();
	  
  }

  @AfterMethod
  public void afterMethod() {
	  
	  driver.quit();
	  
  }
     
}
