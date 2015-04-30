package automationFramework;

import java.util.Set;
import java.util.concurrent.TimeUnit;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.Assert;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.AfterMethod;

import pageObjects.Divuach_Headrut;
import pageObjects.Employee_Card;
import pageObjects.Work_Card;

public class Test_Add_Absences {
	
	public  WebDriver driver;
	
	
	
  @Test
  public void f() throws InterruptedException {
	  
	  
	  
	  
driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	  
	  
	  Base a= new Base();
	  a.waitForWindow("WorkCard",driver);
	  Work_Card.TxtId(driver).sendKeys("85400");
	  Work_Card.Date(driver).click();
	  Work_Card.Date(driver).sendKeys("26042015");
	  Work_Card.Btn_Show(driver).click();
	  Thread.sleep(2000);
	  Divuach_Headrut.Btn_Add_Absence(driver).click();
	  Base b= new Base();
	  b.waitForWindow("DivuachHeadrut",driver);
	  Select droplist = new Select(Divuach_Headrut.List_Absences(driver));
      droplist.selectByVisibleText("היעדרות - תשלום יום עבודה"); 
      Divuach_Headrut.Btn_Update_Absence(driver).click();
      Base c= new Base();
	  c.waitForWindow("WorkCard",driver);
	  System.out.println(driver.getCurrentUrl());
      Assert.assertEquals(Work_Card.Sidur_Num(driver).getText(),"99801");
	  Work_Card.Cancel_Sidur(driver).click();
      Work_Card.Btn_Update(driver);
      Base d= new Base();
	  d.waitForWindow("DivuachHeadrut",driver);
	  
      
     
	  
	  
	  
	  
	  
	  
  }
  
  
  
  
  
  
  
  private void waitForNumberofWindowsToEqual(int i) {
	// TODO Auto-generated method stub
	
}







@BeforeMethod
  public void beforeMethod() {
	  
	  driver=Base.Initialize_browser();
	  Base.Initialize_Webpage(driver);
	  Base.Enter_Workcard(driver);
	  
	  
	  
  }

  
  
  
  
  @AfterMethod
  public void afterMethod() {
  }

}
