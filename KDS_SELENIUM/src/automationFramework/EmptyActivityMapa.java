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
import org.testng.annotations.AfterMethod;

import pageObjects.WorkCard;
import utils.Base;
import utils.Utilsfn;






@Listeners ({Listener.TestListener.class})
public class EmptyActivityMapa   extends Base {
	
	public WebDriver driver;	
	
	
	

	
	
	
	
	
  @Test  
  public void chkEmptyActivityMapa() throws InterruptedException {
	  
	  driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	  
	  
	  Utilsfn a= new Utilsfn();
	  a.waitForWindow("WorkCard",driver);
	  WorkCard.TxtId(driver).sendKeys("77104");
	  WorkCard.Date(driver).click();
	  WorkCard.Date(driver).sendKeys("01042015");
	  WorkCard.Btn_Show(driver).click();
	  WorkCard.AddRekaUp_mapa(driver).click();
	  //Thread.sleep(2000);
	  WorkCard.Wait_For_Element_Stalenes(driver,"SD_000_ctl03_SD_000_ctl03MakatNumber",null);
	  System.out.println(WorkCard.Makat_Num_Reka_Mapa(driver).getAttribute("value"));
	  Assert.assertEquals("60375800",WorkCard.Makat_Num_Reka_Mapa(driver).getAttribute("value"));
	  WorkCard.AddRekadw_mapa(driver).click();
	  WebDriverWait wait = new WebDriverWait(driver, 30);
	  wait.until(ExpectedConditions.alertIsPresent());
	  Alert alert=driver.switchTo().alert();
	  System.out.println(alert.getText());
	  Assert.assertEquals("לא נמצאה נסיעה ריקה שתוכננה במפה",alert.getText());
	  alert.accept();
	  WorkCard.Cancel_Empty_Activity_Mapa(driver).click();
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
