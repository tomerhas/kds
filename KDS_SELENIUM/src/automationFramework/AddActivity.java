package automationFramework;

import java.util.concurrent.TimeUnit;


import org.openqa.selenium.Keys;
import org.openqa.selenium.WebDriver;
import org.testng.Assert;
import org.testng.annotations.Listeners;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import pageObjects.WorkCard;
import utils.Base;
import utils.Utilsfn;




@Listeners ({Listener.TestListener.class})
public class AddActivity   extends Base {
	
	public  WebDriver driver;
	

	
	
  @Test
  public void addActivity() {
	  
	  
	  driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	  Utilsfn a =new Utilsfn();
	  a.waitForWindow("WorkCard",driver);
	  WorkCard.TxtId(driver).sendKeys("77104");
	  WorkCard.Date(driver).click();
	  WorkCard.Date(driver).sendKeys("17052015");
	  WorkCard.Btn_Show(driver).click();
	  WorkCard.Wait_For_Element_Visibile(driver,60,"SD_imgAddPeilut0").click();
	  WorkCard.Wait_For_Element_Visibile(driver,60,"SD_000_ctl08_SD_000_ctl08ShatYetiza").clear();
	  WorkCard.Entry_Time_Add_Activity(driver).sendKeys("1514");
	  WorkCard.Makat_Num_Add_Activity(driver).click();
	  WorkCard.Makat_Num_Add_Activity(driver).sendKeys("79100500");
	  WorkCard.Makat_Num_Add_Activity(driver).sendKeys(Keys.TAB);
	  System.out.println(WorkCard.Assert_Activity_Car_No(driver).getAttribute("value"));
	  Assert.assertEquals(WorkCard.Assert_Activity_Car_No(driver).getAttribute("value"),"62505");
	  WorkCard.Btn_Update(driver).click();
      WorkCard.Wait_For_Element_Stalenes(driver,"SD_000_ctl08_SD_000_ctl08CancelPeilut",null);
	  WorkCard.Cancel_Sidur_Add_Activity(driver).click();
	  WorkCard.Btn_Update(driver).click();
	  WorkCard.Wait_For_Element_Stalenes(driver,"btnCloseCard",null);
	  WorkCard.Btn_Close(driver).click();
	  
	  
	  
	  
	  
	  
	  
	  
	  
	  
	  
  }
  
  
  
  
  
  
  @BeforeMethod
  public void beforeMethod() {
	  
	  //driver=Utils.Initialize_browser();
	  //Utils.Initialize_Webpage(driver);
	  
	  driver=getDriver();
	  Utilsfn.Enter_Workcard(driver);
	  
	  
  }

  
  
  

  
  


}
