package automationFramework;

import java.sql.SQLException;
import java.util.concurrent.TimeUnit;

import org.openqa.selenium.Keys;
import org.openqa.selenium.WebDriver;
import org.testng.Assert;
import org.testng.annotations.Listeners;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;

import JDBC.DB_DML;
import pageObjects.WorkCard;
import utils.Base;
import utils.Utilsfn;










@Listeners ({Listener.TestListener.class})
public class AddSpecialSchedule  extends Base {
	
	public  WebDriver driver;
	
	
  @Test
  public void addSpecialSchedule () throws SQLException {
	  
	  driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	  DB_DML.deleteRecordFromTable("77104","to_date('15/11/2016','dd/mm/yyyy')","99818");
	  Utilsfn a =new Utilsfn();
	  a.waitForWindow("WorkCard",driver);
	  WorkCard.TxtId(driver).sendKeys("77104");
	  WorkCard.Date(driver).click();
	  WorkCard.Date(driver).sendKeys("15112016");
	  WorkCard.Btn_Show(driver).click();
	  WorkCard.Wait_For_Element_Stalenes(driver, "btnAddMyuchad",null);
	  WorkCard.Btn_Add_Special(driver).click();
	  Assert.assertFalse(WorkCard.Start_Time_Special(driver).isEnabled(),"Start_Time_Special is Enabled ");
	  Assert.assertFalse(WorkCard.End_Time_Special(driver).isEnabled(),"End_Time_Special is Enabled ");
	  Assert.assertTrue(WorkCard.Cancel_Schedule_02(driver).isEnabled(),"Cancel_Special_Schedule is Disabled ");
	  WorkCard.Lbl_Sidur_No_2(driver).sendKeys("99002");
	  Assert.assertFalse(WorkCard.Add_Activity_Special(driver).isDisplayed(),"Add_Activity_Special is Displayed ");
	  WorkCard.Lbl_Sidur_No_2(driver).clear();
	  WorkCard.Lbl_Sidur_No_2(driver).sendKeys("99200");
	  WorkCard.Lbl_Sidur_No_2(driver).sendKeys(Keys.TAB);
	  Assert.assertEquals(WorkCard.Validate_Popup(driver).getText(), "לא ניתן לדווח סידור התייצבות");
	  WorkCard.Lbl_Sidur_No_2(driver).sendKeys("99850");
	  WorkCard.Lbl_Sidur_No_2(driver).sendKeys(Keys.TAB);
	  Assert.assertEquals(WorkCard.Validate_Popup(driver).getText(), "יש לדווח במסך הוסף דיווח היעדרות");
	  WorkCard.Lbl_Sidur_No_2(driver).sendKeys("11111");
	  WorkCard.Lbl_Sidur_No_2(driver).sendKeys(Keys.TAB);
	  Assert.assertEquals(WorkCard.Validate_Popup(driver).getText(), "מספר סידור שגוי");
	  WorkCard.Lbl_Sidur_No_2(driver).sendKeys("99301");
	  WorkCard.Lbl_Sidur_No_2(driver).sendKeys(Keys.TAB);
	  Assert.assertTrue(WorkCard.Add_Activity_Special(driver).isEnabled(),"Add_Activity_Special is Disabled ");
	  WorkCard.Start_Time_Special(driver).sendKeys("2300");
	  WorkCard.Start_Time_Special(driver).sendKeys(Keys.TAB);
	  WorkCard.End_Time_Special(driver).sendKeys("2345");
	  WorkCard.Btn_Update(driver).click();
	  WorkCard.Wait_For_Element_Stalenes(driver, "SD_imgCancel2",null);
	  WorkCard.Cancel_Schedule_02(driver).click();
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
