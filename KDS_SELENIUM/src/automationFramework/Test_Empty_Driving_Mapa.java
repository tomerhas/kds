package automationFramework;

import java.util.concurrent.TimeUnit;

import org.junit.Assert;
import org.openqa.selenium.Alert;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.AfterMethod;

import pageObjects.Work_Card;

public class Test_Empty_Driving_Mapa {
	
	public WebDriver driver;	
	
	
	

	
	
	
	
	
  @Test  
  public void f() throws InterruptedException {
	  
	  driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	  
	  
	  Base a= new Base();
	  a.waitForWindow("WorkCard",driver);
	  Work_Card.TxtId(driver).sendKeys("77104");
	  Work_Card.Date(driver).click();
	  Work_Card.Date(driver).sendKeys("01042015");
	  Work_Card.Btn_Show(driver).click();
	  Work_Card.AddRekaUp_mapa(driver).click();
	  Thread.sleep(2000);
	  System.out.println(Work_Card.Makat_Num(driver).getAttribute("value"));
	  Assert.assertEquals("60375800",Work_Card.Makat_Num(driver).getAttribute("value"));
	  Work_Card.AddRekadw_mapa(driver).click();
	  WebDriverWait wait = new WebDriverWait(driver, 30);
	  wait.until(ExpectedConditions.alertIsPresent());
	  Alert alert=driver.switchTo().alert();
	  System.out.println(alert.getText());
	  Assert.assertEquals("לא נמצאה נסיעה ריקה שתוכננה במפה",alert.getText());
	  alert.accept();
	  Work_Card.Cancel_Activity(driver).click();
	  Work_Card.Btn_Update(driver).click();
	  Thread.sleep(3000);
	  Work_Card.Btn_Close(driver).click();
	  
	 
	  
	  
	  
	  
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
