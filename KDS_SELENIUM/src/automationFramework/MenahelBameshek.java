package automationFramework;

import java.util.concurrent.TimeUnit;

import JDBC.DB_DML;

import org.openqa.selenium.Alert;
import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.Keys;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.Assert;
import org.testng.annotations.Listeners;
import org.testng.annotations.Test;
import pageObjects.EmployeeCard;
import pageObjects.LogInPage;
import pageObjects.WorkCard;
import utils.Utilsfn;
import utils.Base;

@Listeners ({Listener.TestListener.class})
public class MenahelBameshek    extends Base {
	  public WebDriver driver;
	  
	  
	  
	
	
	
	
	
	
	
  @Test
  public void  chkMenahelBameshek() throws Exception      {
	  driver=getDriver();
	
	  
	  driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	  DB_DML.deleteRecordFromTable("46194","to_date('24/11/2016','dd/mm/yyyy')","99001");
	  driver.manage().window().maximize();
	  LogInPage.Txt_Change_User(driver).sendKeys("igalr");
	  LogInPage.Btn_Change_User(driver).click();
	  WorkCard.Wait_For_Element_Stalenes(driver,"ctl00_KdsContent_btnUpdWorkCard",null);
	  LogInPage.lnk_EmployeeCards(driver).click();
	  EmployeeCard.Txt_Id(driver).clear();
	  EmployeeCard.Txt_Id(driver).sendKeys("87903");
	  EmployeeCard.Txt_Id(driver).sendKeys(Keys.TAB);
	  WebDriverWait wait = new WebDriverWait(driver, 30);
	  wait.until(ExpectedConditions.alertIsPresent());
      Alert alert=driver.switchTo().alert();
      Assert.assertEquals("מספר אישי לא קיים/אינך מורשה לצפות בעובד זה", alert.getText());
	  alert.accept();
	  EmployeeCard.Txt_Id(driver).sendKeys("46194");
	  EmployeeCard.Txt_Id(driver).sendKeys(Keys.TAB);
	  Select droplist = new Select(EmployeeCard.List_Month(driver));
      droplist.selectByVisibleText("11/2016"); 
      EmployeeCard.Btn_Execute(driver).click();
      EmployeeCard.Link_Date_Menahel_Bameshek(driver).click();
      Utilsfn a =new Utilsfn();
	  a.waitForWindow("WorkCard",driver);
      Assert.assertFalse(WorkCard.Btn_Add_Absence(driver).isEnabled(),"Btn_Add_Absence is Enabled ");
      Assert.assertFalse(WorkCard.Btn_Add_Mapa(driver).isEnabled(),"Btn_Add_Mapa is Enabled ");
      Assert.assertFalse(WorkCard.BtnCalcItems(driver).isEnabled(),"Btn_Calc_Items is Enabled ");
      Assert.assertTrue(WorkCard.Btn_Add_Special(driver).isEnabled(),"Btn_Add_Special is Disabled ");	
      Assert.assertTrue(WorkCard.Btn_Clocks(driver).isEnabled(),"Btn_Clocks is Disabled ");			
      Assert.assertTrue(WorkCard.BtnDriverErrors(driver).isEnabled(),"Btn_Driver_Errors is Disabled ");
      System.out.println(WorkCard.Assert_Sidur_disabled(driver).getAttribute("readonly"));
      Assert.assertTrue(true,WorkCard.Assert_Sidur_disabled(driver).getAttribute("readonly"));
      WorkCard.Btn_Add_Special(driver).click();
      WorkCard.Lbl_Sidur_No_2(driver).sendKeys("99002");
	  WorkCard.Lbl_Sidur_No_2(driver).sendKeys(Keys.TAB);
	  Assert.assertEquals(WorkCard.Validate_Popup(driver).getText(),"אינך רשאי לדווח סידור עבודה זה");
	  WorkCard.Lbl_Sidur_No_2(driver).sendKeys("99850");
	  WorkCard.Lbl_Sidur_No_2(driver).sendKeys(Keys.TAB);
	  Assert.assertEquals(WorkCard.Validate_Popup(driver).getText(),"יש לדווח במסך הוסף דיווח היעדרות");
	  WorkCard.Lbl_Sidur_No_2(driver).sendKeys("11111");
	  WorkCard.Lbl_Sidur_No_2(driver).sendKeys(Keys.TAB);
	  Assert.assertEquals(WorkCard.Validate_Popup(driver).getText(), "מספר סידור שגוי");
	  WorkCard.Lbl_Sidur_No_2(driver).sendKeys("99001");
	  WorkCard.Lbl_Sidur_No_2(driver).sendKeys(Keys.TAB);
	  WorkCard.Start_Time_Special(driver).sendKeys("1200");
	  WorkCard.Start_Time_Special(driver).sendKeys(Keys.TAB);
	  WorkCard.End_Time_Special(driver).sendKeys("1400");
	  WorkCard.End_Time_Special(driver).sendKeys(Keys.TAB);
	  Select droplist1 = new Select(WorkCard.List_Reason_In(driver));
      droplist1.selectByVisibleText("שעון לא מדויק/לא תקין");     
      Select droplist2 = new Select(WorkCard.List_Reason_Out(driver));
      droplist2.selectByVisibleText("שעון לא מדויק/לא תקין");
      WorkCard.Btn_Update(driver).click();      
	  WorkCard.Wait_For_Element_Stalenes(driver,"clnDate",null);
	  WorkCard.Date(driver).click();
	  WorkCard.Date(driver).sendKeys("21112016");
	  WorkCard.Btn_Show(driver).click();
	  WorkCard.Wait_For_Element_Stalenes(driver,"btnAddMyuchad",null);
	  WorkCard.Btn_Add_Special(driver).click();
      WorkCard.Lbl_Sidur_No_2(driver).sendKeys("99300");
	  WorkCard.Lbl_Sidur_No_2(driver).sendKeys(Keys.TAB);
	  
	  //Work_Card.Btn_Update(driver).click();
	  //WebDriverWait wait1 = new WebDriverWait(driver, 30);
	  //wait1.until(ExpectedConditions.alertIsPresent());
	  //Alert alert1=driver.switchTo().alert();
	  //System.out.println(alert1.getText());
	  //Assert.assertEquals("מספר סידור  אינו תקין",alert1.getText());
	  //alert1.accept();
	  Assert.assertEquals(WorkCard.Validate_Popup(driver).getText(),"אינך רשאי לדווח סידור עבודה זה");
	  WorkCard.Btn_Close(driver).click();
	  JavascriptExecutor js = (JavascriptExecutor)driver; 
      js.executeScript("arguments[0].click();", WorkCard.Btn_Cancel_Update(driver)); 
      
	  
     
	  
  }

  

  
 
  
 /* 
  
  @BeforeMethod
  public void beforeMethod() {
	  
	  driver=Base.Initialize_browser();
	  Base.Initialize_Webpage(driver);
	  //driver.get("http://igalr:DD2468@kdstest");
	  
	  
	  
  }

  
  
  
  
  
  
  
  
  
  @AfterMethod
  public void afterMethod() {
	  
	
	  driver.quit();
	  
	  
  }*/

}
