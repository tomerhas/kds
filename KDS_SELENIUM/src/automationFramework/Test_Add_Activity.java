package automationFramework;

import java.util.concurrent.TimeUnit;


import org.openqa.selenium.By;
import org.openqa.selenium.ElementNotVisibleException;
import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.Keys;
import org.openqa.selenium.StaleElementReferenceException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.ExpectedCondition;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.Assert;
import org.testng.annotations.Listeners;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.AfterMethod;
import pageObjects.Work_Card;
import utils.Base;
import utils.Utils;




@Listeners ({Listener.TestListener.class})
public class Test_Add_Activity   extends Base {
	
	public  WebDriver driver;
	

	
	
  @Test
  public void Add_Activity() {
	  
	  
	  driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	  Utils a =new Utils();
	  a.waitForWindow("WorkCard",driver);
	  Work_Card.TxtId(driver).sendKeys("77104");
	  Work_Card.Date(driver).click();
	  Work_Card.Date(driver).sendKeys("17052015");
	  Work_Card.Btn_Show(driver).click();
	  Work_Card.Wait_For_Element_Visibile(driver,60,"SD_imgAddPeilut0").click();
	  Work_Card.Wait_For_Element_Visibile(driver,60,"SD_000_ctl08_SD_000_ctl08ShatYetiza").clear();
	  Work_Card.Entry_Time_Add_Activity(driver).sendKeys("1514");
	  Work_Card.Makat_Num_Add_Activity(driver).click();
	  Work_Card.Makat_Num_Add_Activity(driver).sendKeys("79100500");
	  Work_Card.Makat_Num_Add_Activity(driver).sendKeys(Keys.TAB);
	  System.out.println(Work_Card.Assert_Activity_Car_No(driver).getAttribute("value"));
	  Assert.assertEquals(Work_Card.Assert_Activity_Car_No(driver).getAttribute("value"),"62505");
	  Work_Card.Btn_Update(driver).click();
      Work_Card.Wait_For_Element_Stalenes(driver,"SD_000_ctl08_SD_000_ctl08CancelPeilut");
	  Work_Card.Cancel_Sidur_Add_Activity(driver).click();
	  Work_Card.Btn_Update(driver).click();
	  Work_Card.Wait_For_Element_Stalenes(driver,"btnCloseCard");
	  Work_Card.Btn_Close(driver).click();
	  
	  
	  
	  
	  
	  
	  
	  
	  
	  
	  
  }
  
  
  
  
  
  
  @BeforeMethod
  public void beforeMethod() {
	  
	  //driver=Utils.Initialize_browser();
	  //Utils.Initialize_Webpage(driver);
	  
	  driver=getDriver();
	  Utils.Enter_Workcard(driver);
	  
	  
  }

  
  
  

  
  


}
