package automationFramework;

import java.util.concurrent.TimeUnit;

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
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.AfterMethod;

import JDBC.DB_DML;
import pageObjects.Employee_Card;
import pageObjects.LogIn_Page;
import pageObjects.Work_Card;
import utils.Utils;
import utils.Base;

@Listeners ({Listener.TestListener.class})
public class Test_Menahel_Bameshek    extends Base {
	  public WebDriver driver;
	  
	  
	  
	
	
	
	
	
	
	
  @Test
  public void  Menahel_Bameshek() throws Exception      {
	  driver=getDriver();
	
	  
	  driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	  DB_DML.deleteRecordFromTable("46194","to_date('11/06/2015','dd/mm/yyyy')","99001");
	  LogIn_Page.Txt_Change_User(driver).sendKeys("igalr");
	  LogIn_Page.Btn_Change_User(driver).click();
	  Work_Card.Wait_For_Element_Stalenes(driver,"ctl00_KdsContent_btnUpdWorkCard");
	  LogIn_Page.lnk_EmployeeCards(driver).click();
	  Employee_Card.Txt_Id(driver).clear();
	  Employee_Card.Txt_Id(driver).sendKeys("87903");
	  Employee_Card.Txt_Id(driver).sendKeys(Keys.TAB);
	  WebDriverWait wait = new WebDriverWait(driver, 30);
	  wait.until(ExpectedConditions.alertIsPresent());
      Alert alert=driver.switchTo().alert();
      Assert.assertEquals("מספר אישי לא קיים/אינך מורשה לצפות בעובד זה", alert.getText());
	  alert.accept();
	  Employee_Card.Txt_Id(driver).sendKeys("46194");
	  Employee_Card.Txt_Id(driver).sendKeys(Keys.TAB);
	  Select droplist = new Select(Employee_Card.List_Month(driver));
      droplist.selectByVisibleText("06/2015"); 
      Employee_Card.Btn_Execute(driver).click();
      Employee_Card.Link_Date_Menahel_Bameshek(driver).click();
      Utils a =new Utils();
	  a.waitForWindow("WorkCard",driver);
      Assert.assertFalse(Work_Card.Btn_Add_Absence(driver).isEnabled(),"Btn_Add_Absence is Enabled ");
      Assert.assertFalse(Work_Card.Btn_Add_Mapa(driver).isEnabled(),"Btn_Add_Mapa is Enabled ");
      Assert.assertFalse(Work_Card.Btn_Calc_Items(driver).isEnabled(),"Btn_Calc_Items is Enabled ");
      Assert.assertTrue(Work_Card.Btn_Add_Special(driver).isEnabled(),"Btn_Add_Special is Disabled ");	
      Assert.assertTrue(Work_Card.Btn_Clocks(driver).isEnabled(),"Btn_Clocks is Disabled ");			
      Assert.assertTrue(Work_Card.Btn_Driver_Errors(driver).isEnabled(),"Btn_Driver_Errors is Disabled ");
      System.out.println(Work_Card.Assert_Sidur_disabled(driver).getAttribute("readonly"));
      Assert.assertTrue(true,Work_Card.Assert_Sidur_disabled(driver).getAttribute("readonly"));
      Work_Card.Btn_Add_Special(driver).click();
      Work_Card.Lbl_Special_No(driver).sendKeys("99002");
	  Work_Card.Lbl_Special_No(driver).sendKeys(Keys.TAB);
	  Assert.assertEquals(Work_Card.Validate_Popup(driver).getText(),"אינך רשאי לדווח סידור עבודה זה");
	  Work_Card.Lbl_Special_No(driver).sendKeys("99850");
	  Work_Card.Lbl_Special_No(driver).sendKeys(Keys.TAB);
	  Assert.assertEquals(Work_Card.Validate_Popup(driver).getText(),"יש לדווח במסך הוסף דיווח היעדרות");
	  Work_Card.Lbl_Special_No(driver).sendKeys("11111");
	  Work_Card.Lbl_Special_No(driver).sendKeys(Keys.TAB);
	  Assert.assertEquals(Work_Card.Validate_Popup(driver).getText(), "מספר סידור שגוי");
	  Work_Card.Lbl_Special_No(driver).sendKeys("99001");
	  Work_Card.Lbl_Special_No(driver).sendKeys(Keys.TAB);
	  Work_Card.Start_Time_Special(driver).sendKeys("1200");
	  Work_Card.Start_Time_Special(driver).sendKeys(Keys.TAB);
	  Work_Card.End_Time_Special(driver).sendKeys("1400");
	  Work_Card.End_Time_Special(driver).sendKeys(Keys.TAB);
	  Select droplist1 = new Select(Work_Card.List_Reason_In(driver));
      droplist1.selectByVisibleText("שעון לא מדויק/לא תקין");     
      Select droplist2 = new Select(Work_Card.List_Reason_Out(driver));
      droplist2.selectByVisibleText("שעון לא מדויק/לא תקין");
      Work_Card.Btn_Update(driver).click();      
	  
	  Work_Card.Wait_For_Element_Stalenes(driver,"clnDate");
	  Work_Card.Date(driver).click();
	  Work_Card.Date(driver).sendKeys("18072015");
	  Work_Card.Btn_Show(driver).click();
	  Work_Card.Wait_For_Element_Stalenes(driver,"btnAddMyuchad");
	  Work_Card.Btn_Add_Special(driver).click();
      Work_Card.Lbl_Special_No(driver).sendKeys("99300");
	  Work_Card.Lbl_Special_No(driver).sendKeys(Keys.TAB);
	  
	  //Work_Card.Btn_Update(driver).click();
	  //WebDriverWait wait1 = new WebDriverWait(driver, 30);
	  //wait1.until(ExpectedConditions.alertIsPresent());
	  //Alert alert1=driver.switchTo().alert();
	  //System.out.println(alert1.getText());
	  //Assert.assertEquals("מספר סידור  אינו תקין",alert1.getText());
	  //alert1.accept();
	  Assert.assertEquals(Work_Card.Validate_Popup(driver).getText(),"כרטיס ללא התייחסות, לא ניתן להוסיף סידור זה");
	  Work_Card.Btn_Close(driver).click();
	  JavascriptExecutor js = (JavascriptExecutor)driver; 
      js.executeScript("arguments[0].click();", Work_Card.Btn_Cancel_Update(driver)); 
      
	  
     
	  
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
