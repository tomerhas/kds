package automationFramework;

import java.util.concurrent.TimeUnit;

import org.openqa.selenium.Alert;
import org.openqa.selenium.Keys;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.Assert;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.AfterMethod;

import JDBC.DB_DML;
import pageObjects.Employee_Card;
import pageObjects.LogIn_Page;
import pageObjects.Work_Card;

import java.sql.SQLException;


public class Test_Menahel_Bameshek {
	
	
	public WebDriver driver;
	
	
  @Test
  public void f() throws InterruptedException, SQLException {
	  
	  
	  //DB_DML.deleteRecordFromTable("65929","to_date('09/06/2015','dd/mm/yyyy')","99001");
	  driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	  LogIn_Page.lnk_EmployeeCards(driver).click();
	  Employee_Card.Rdo_Name(driver).click();
	  //Thread.sleep(2000);
	  //Work_Card.Wait_For_Element_Stalenes(driver,"ctl00_ImageHome");
	  LogIn_Page.lnk_Home_Page(driver).click();
	  LogIn_Page.lnk_EmployeeCards(driver).click();
	  Employee_Card.Txt_Id(driver).clear();
	  Employee_Card.Txt_Id(driver).sendKeys("87903");
	  Employee_Card.Txt_Id(driver).sendKeys(Keys.TAB);
	  WebDriverWait wait = new WebDriverWait(driver, 30);
	  wait.until(ExpectedConditions.alertIsPresent());
      Alert alert=driver.switchTo().alert();
      Assert.assertEquals("מספר אישי לא קיים/אינך מורשה לצפות בעובד זה", alert.getText());
	  alert.accept();
	  
	
		
			
			
			
	 
	  
  }
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  @BeforeMethod
  public void beforeMethod() {
	  
	  driver=Base.Initialize_browser();
	  driver.get("http://igalr:DD2468@kdstest");
	  
	  
	  
  }

  
  
  
  
  
  
  
  
  
  
  
  @AfterMethod
  public void afterMethod() {
  }

}
