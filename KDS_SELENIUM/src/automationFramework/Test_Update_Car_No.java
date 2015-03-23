package automationFramework;

import java.util.concurrent.TimeUnit;

import org.openqa.selenium.Alert;
import org.openqa.selenium.By;
import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.Assert;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.AfterMethod;

import pageObjects.Work_Card;

public class Test_Update_Car_No {
	
	public WebDriver driver;	
	 
	
	
	 @DataProvider(name = "Param_Car")
	  public Object[][] Parameters_Car_No() {
	          return new Object[][] {
	              {"11111"},
	              { "34918"},
	              { "22228"}
	            
	            
	           };
	                    
	          };
	
	
  @Test (dataProvider="Param_Car")
  
  public void f( String sCar_No  )   throws InterruptedException {
	  
	      for (String handle : driver.getWindowHandles()) {
	      driver.switchTo().window(handle);}
	      
	      //((JavascriptExecutor) driver).executeScript("window.showModalDialog = window.open;");
		  driver.manage().timeouts().implicitlyWait(30, TimeUnit.SECONDS);
		 
		  //WebDriverWait wait = new WebDriverWait(driver, 150);
		  //wait.until(ExpectedConditions.textToBePresentInElementValue(Work_Card.TxtId(driver), "31777"));
		  Thread.sleep(3000);
		  
		  Work_Card.TxtId(driver).sendKeys("77104");
		  Work_Card.Date(driver).sendKeys("03/03/2015");
		  Work_Card.Btn_Show(driver).click();
		  Thread.sleep(3000);	
		  Work_Card.Car_Num(driver).click();
	      Work_Card.Car_Num(driver).sendKeys(sCar_No);
	      if (sCar_No=="11111") {
	      Assert.assertEquals(Work_Card.Validate_Car_No(driver).getText(),"מספר רכב שגוי");
	      
	      Work_Card.Btn_Close(driver).click();
	      
	      //wait.until(ExpectedConditions.elementToBeClickable(Work_Card.Btn_Cancel_Update(driver)));
	      //WebElement element = driver.findElement(By.id("btnCancel"));
	      JavascriptExecutor js = (JavascriptExecutor)driver; 
	      js.executeScript("arguments[0].click();", Work_Card.Btn_Cancel_Update(driver)); 
	      //Work_Card.Btn_Cancel_Update(driver).click();
	      }
	      
	      else {
	      Work_Card.Btn_No_Copy_Car_Num(driver).click();
	      System.out.println(Work_Card.Assert_Car_Num(driver).getAttribute("value"));
	      if (sCar_No=="34918"){
	      Assert.assertEquals(Work_Card.Assert_Car_Num(driver).getAttribute("value"),"22228");
	      }
	      else {
	      Assert.assertEquals(Work_Card.Assert_Car_Num(driver).getAttribute("value"),"34918");	
	      }
	      Work_Card.Car_Num(driver).click();
	      Work_Card.Car_Num(driver).sendKeys(sCar_No);
	      Work_Card.Btn_Yes_Copy_Car_Num(driver).click();
	      System.out.println(Work_Card.Assert_Car_Num(driver).getAttribute("value"));
	      if (sCar_No=="34918")
	      Assert.assertEquals(Work_Card.Assert_Car_Num(driver).getAttribute("value"),"34918");
	      else {
	      Assert.assertEquals(Work_Card.Assert_Car_Num(driver).getAttribute("value"),"22228");
          }
	      Work_Card.Btn_Update(driver).click();
	      Thread.sleep(3000);
	      
	      Work_Card.Btn_Close(driver).click();
	      }
	      
	      
	      
	  	  
  }
  
  
  
  
  
  @BeforeMethod
  public void beforeMethod() {
	  
	  
	  driver=Base.Initialize_browser();
	  Base.Initialize_Webpage(driver);
	  Base.Enter_Workcard(driver);
	  
	  
  }

  
  
    
  
  
  @AfterMethod
  public void afterMethod() {
	  driver.quit();
  }
   
}
