package automationFramework;

import java.io.File;
import java.util.concurrent.TimeUnit;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.ie.InternetExplorerDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.Assert;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.AfterMethod;

import pageObjects.Employee_Card;
import pageObjects.LogIn_Page;
import pageObjects.Work_Card;

import java.lang.Thread;




public class Test_Update_Day_WorkCard {

	public WebDriver driver;
	public WebElement element;
	
	 @DataProvider(name = "Param_dd")
	  public Object[][] Parameters_dd() {
	          return new Object[][] {
	            { "חסר טכוגרף","לינה אחת"},
	        	{ "צירף טכוגרף","אין לינה"}
	           
	           };
	                    
	          };
	
	
	
	
	
	
  @Test ( dataProvider = "Param_dd")
  
	
  
 public void f(String sTachograph,String sLina ) throws InterruptedException {




      for (String handle : driver.getWindowHandles()) {
      driver.switchTo().window(handle);}
	  driver.manage().timeouts().implicitlyWait(50, TimeUnit.SECONDS);
	  //WebDriverWait wait = new WebDriverWait(driver, 350);
	  //wait.until(ExpectedConditions.textToBePresentInElementValue(Work_Card.TxtId(driver), "31777"));
	  Thread.sleep(3000);
	  Work_Card.TxtId(driver).sendKeys("77104");
	  Work_Card.Date(driver).sendKeys("03/03/2015");
	  Work_Card.Btn_Show(driver).click();
	  Thread.sleep(3000);
	  Work_Card.Day_Plus(driver).click();
	  Select droplist = new Select(Work_Card.Tachograph(driver));
      droplist.selectByVisibleText(sTachograph); 
      Select droplist1 = new Select(Work_Card.Lina(driver));
      droplist1.selectByVisibleText(sLina); 
      Assert.assertFalse(Work_Card.Hamara(driver).isEnabled(),"The Checkbox Hamara is Enabled");
      Assert.assertFalse(Work_Card.Halbasha(driver).isEnabled(),"The Checkbox Halbasha is Enabled");
      Assert.assertFalse(Work_Card.HashlamaForDay(driver).isEnabled(),"The Checkbox HashlamaForDay is Enabled");
      Assert.assertFalse(Work_Card.HashlamaReason(driver).isEnabled(),"The Checkbox HashlamaReason is Enabled");
      Work_Card.Btn_Update(driver).click();
      Thread.sleep(3000);
      Work_Card.Btn_Close(driver).click();
	  
	  
  }
  
  
  
  

  





@BeforeMethod
  public void beforeMethod() {
	  
	 
	  driver = Base.Initialize_browser();
	  Base.Initialize_Webpage(driver);
	  Base.Enter_Workcard(driver);
	  
      
      
  }

  @AfterMethod
  public void afterMethod() {
	  
	  driver.quit();
	  
  }
     
}
