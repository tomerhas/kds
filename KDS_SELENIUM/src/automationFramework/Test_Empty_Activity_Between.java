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

public class Test_Empty_Activity_Between {
	
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
	  Work_Card.Assert_Reka_Between_Not_Able(driver).click();
	  WebDriverWait wait = new WebDriverWait(driver, 30);
	  wait.until(ExpectedConditions.alertIsPresent());
	  Alert alert=driver.switchTo().alert();
	  System.out.println(alert.getText());
	  Assert.assertEquals("לא ניתן להשלים נסיעה ריקה",alert.getText());
	  alert.accept();
	  Work_Card.Assert_Reka_Between_Not_Found(driver).click();
	  wait.until(ExpectedConditions.alertIsPresent());
	  Alert alert1=driver.switchTo().alert();
	  System.out.println(alert.getText());
	  Assert.assertEquals("לא נמצאה ריקה מתאימה",alert1.getText());
	  alert.accept();
	  Work_Card.Add_Reka_Between(driver).click();
	  Thread.sleep(2000);
	  Assert.assertEquals("61589900",Work_Card.Makat_Num_Reka_Between(driver).getAttribute("value"));
	  Work_Card.Cancel__Empty_Activity_Between(driver).click();
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
