package automationFramework;

import java.util.concurrent.TimeUnit;

import org.openqa.selenium.Keys;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.Select;
import org.testng.Assert;
import org.testng.annotations.Listeners;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.AfterMethod;

import pageObjects.Work_Card;
import utils.Base;
import utils.Utils;










@Listeners ({Listener.TestListener.class})
public class Test_Add_Special_Schedule  extends Base {
	
	public  WebDriver driver;
	
	
  @Test
  public void Test_Add_Special_Schedule () {
	  
	  driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	  Utils a =new Utils();
	  a.waitForWindow("WorkCard",driver);
	  Work_Card.TxtId(driver).sendKeys("77104");
	  Work_Card.Date(driver).click();
	  Work_Card.Date(driver).sendKeys("20052015");
	  Work_Card.Btn_Show(driver).click();
	  Work_Card.Wait_For_Element_Stalenes(driver, "btnAddMyuchad");
	  Work_Card.Btn_Add_Special(driver).click();
	  Assert.assertFalse(Work_Card.Start_Time_Special(driver).isEnabled(),"Start_Time_Special is Enabled ");
	  Assert.assertFalse(Work_Card.End_Time_Special(driver).isEnabled(),"End_Time_Special is Enabled ");
	  Assert.assertTrue(Work_Card.Cancel_Schedule_02(driver).isEnabled(),"Cancel_Special_Schedule is Disabled ");
	  Work_Card.Lbl_Special_No(driver).sendKeys("99002");
	  Assert.assertFalse(Work_Card.Add_Activity_Special(driver).isDisplayed(),"Add_Activity_Special is Displayed ");
	  Work_Card.Lbl_Special_No(driver).clear();
	  Work_Card.Lbl_Special_No(driver).sendKeys("99200");
	  Work_Card.Lbl_Special_No(driver).sendKeys(Keys.TAB);
	  Assert.assertEquals(Work_Card.Validate_Popup(driver).getText(), "לא ניתן לדווח סידור התייצבות");
	  Work_Card.Lbl_Special_No(driver).sendKeys("99850");
	  Work_Card.Lbl_Special_No(driver).sendKeys(Keys.TAB);
	  Assert.assertEquals(Work_Card.Validate_Popup(driver).getText(), "יש לדווח במסך הוסף דיווח היעדרות");
	  Work_Card.Lbl_Special_No(driver).sendKeys("11111");
	  Work_Card.Lbl_Special_No(driver).sendKeys(Keys.TAB);
	  Assert.assertEquals(Work_Card.Validate_Popup(driver).getText(), "מספר סידור שגוי");
	  Work_Card.Lbl_Special_No(driver).sendKeys("99301");
	  Work_Card.Lbl_Special_No(driver).sendKeys(Keys.TAB);
	  Assert.assertTrue(Work_Card.Add_Activity_Special(driver).isEnabled(),"Add_Activity_Special is Disabled ");
	  Work_Card.Start_Time_Special(driver).sendKeys("2300");
	  Work_Card.Start_Time_Special(driver).sendKeys(Keys.TAB);
	  Work_Card.End_Time_Special(driver).sendKeys("2345");
	  Work_Card.Btn_Update(driver).click();
	  Work_Card.Wait_For_Element_Stalenes(driver, "SD_imgCancel2");
	  Work_Card.Cancel_Schedule_02(driver).click();
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
