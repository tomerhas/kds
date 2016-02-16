package automationFramework;

import java.util.concurrent.TimeUnit;

import org.junit.Assert;
import org.openqa.selenium.Alert;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.annotations.Listeners;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import pageObjects.WorkCard;
import utils.Base;
import utils.Utilsfn;



@Listeners ({Listener.TestListener.class})
public class EmptyActivityBetween extends Base {
	
	public WebDriver driver;
	
	
  @Test
  public void chkEmptyActivityBetween() throws InterruptedException {
	  
	  
driver.manage().timeouts().implicitlyWait(15, TimeUnit.SECONDS);
	  
	  
	  Utilsfn a= new Utilsfn();
	  a.waitForWindow("WorkCard",driver);
	  WorkCard.TxtId(driver).sendKeys("77104");
	  WorkCard.Date(driver).click();
	  WorkCard.Date(driver).sendKeys("01042015");
	  WorkCard.Btn_Show(driver).click();
	  WorkCard.Assert_Reka_Between_Not_Able(driver).click();
	  WebDriverWait wait = new WebDriverWait(driver, 40);
	  wait.until(ExpectedConditions.alertIsPresent());
	  Alert alert=driver.switchTo().alert();
	  System.out.println(alert.getText());
	  Assert.assertEquals("לא ניתן להשלים נסיעה ריקה",alert.getText());
	  alert.accept();
	  WorkCard.Assert_Reka_Between_Not_Found(driver).click();
	  wait.until(ExpectedConditions.alertIsPresent());
	  Alert alert1=driver.switchTo().alert();
	  System.out.println(alert.getText());
	  Assert.assertEquals("לא נמצאה ריקה מתאימה",alert1.getText());
	  alert.accept();
	  WorkCard.Add_Reka_Between(driver).click();
	  //Thread.sleep(2000);
	  WorkCard.Wait_For_Element_Visibile(driver, 60, "SD_002_ctl05_SD_002_ctl05MakatNumber");
	  Assert.assertEquals("61589900",WorkCard.Makat_Num_Reka_Between(driver).getAttribute("value"));
	  WorkCard.Cancel__Empty_Activity_Between(driver).click();
	  WorkCard.Btn_Update(driver).click();
	  //Thread.sleep(3000);
	  WorkCard.Wait_For_Element_Stalenes(driver, "btnCloseCard",null);
	  WorkCard.Btn_Close(driver).click();
	  
	  
  }
  
  
  
  
  
  
  @BeforeMethod
  public void beforeMethod() {
	  
	  driver=getDriver();
	  Utilsfn.Enter_Workcard(driver);
	  
  }

  
  
  
	  
	  
  

}
