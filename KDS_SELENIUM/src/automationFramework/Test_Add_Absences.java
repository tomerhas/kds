package automationFramework;

import java.util.concurrent.TimeUnit;

import org.openqa.selenium.Alert;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.Assert;
import org.testng.annotations.Listeners;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.AfterMethod;

import pageObjects.Divuach_Headrut;
import pageObjects.Work_Card;
import utils.Utils;
import utils.Base;




@Listeners ({Listener.TestListener.class})
public class Test_Add_Absences    extends Base {
	
	public  WebDriver driver;
	
	
	
  @Test
  public void Add_Absences() throws InterruptedException {
	  
	  
	  
	  
driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	  
	  
	  Utils a= new Utils();
	  a.waitForWindow("WorkCard",driver);
	  Work_Card.TxtId(driver).sendKeys("85400");
	  Work_Card.Date(driver).click();
	  Work_Card.Date(driver).sendKeys("26042015");
	  Work_Card.Btn_Show(driver).click();
	  //Thread.sleep(2000);
	  Work_Card.Wait_For_Element_Visibile(driver,60,"btnAddHeadrut");
	  Work_Card.Btn_Add_Absence(driver).click();
	  Utils b= new Utils();
	  b.waitForWindow("DivuachHeadrut",driver);
	  Select droplist = new Select(Divuach_Headrut.List_Absences(driver));
      droplist.selectByVisibleText("������� - ����� ��� �����"); 
      Divuach_Headrut.Start_Time_Absences(driver).sendKeys("0600");
      Divuach_Headrut.End_Time_Absences(driver).click();
      Divuach_Headrut.End_Time_Absences(driver).sendKeys("2300");
      Divuach_Headrut.Btn_Update_Absence(driver).click();
      Utils c= new Utils();
	  c.waitForWindow("WorkCard",driver);
	  System.out.println(driver.getCurrentUrl());
      Assert.assertEquals(Work_Card.Sidur_Num(driver).getText(),"99801");
	  Work_Card.Cancel_Sidur(driver).click();
      Work_Card.Btn_Update(driver).click();
      Work_Card.Wait_For_Element_Stalenes(driver, "btnAddHeadrut");
      Work_Card.Btn_Add_Absence(driver).click();
      Utils d= new Utils();
	  d.waitForWindow("DivuachHeadrut",driver);
	  Select droplist1 = new Select(Divuach_Headrut.List_Absences(driver));
      droplist1.selectByVisibleText("�������"); 
      Divuach_Headrut.End_Date_Absences(driver).click();
      Divuach_Headrut.End_Date_Absences(driver).sendKeys("27042015");
      Divuach_Headrut.Btn_Update_Absence(driver).click();
      Utils e= new Utils();
	  e.waitForWindow("WorkCard",driver);
      Assert.assertEquals(Work_Card.Sidur_Num(driver).getText(),"99830");
      Work_Card.Date(driver).click();
	  Work_Card.Date(driver).sendKeys("27042015");
	  Work_Card.Btn_Show(driver).click();
	  //Thread.sleep(2000);
	  Work_Card.Wait_For_Element_Visibile(driver,60,"SD_lblSidur0");
	  //WebDriverWait wait = new WebDriverWait(driver,50);
	  //wait.until(ExpectedConditions.visibilityOf(Work_Card.Sidur_Num(driver)));
	  Assert.assertEquals(Work_Card.Sidur_Num(driver).getText(),"99830");
	  Work_Card.Cancel_Sidur(driver).click();
      Work_Card.Btn_Update(driver).click();
      //Thread.sleep(2000);
      Work_Card.Wait_For_Element_Stalenes(driver, "clnDate");
      Work_Card.Date(driver).click();
	  Work_Card.Date(driver).sendKeys("26042015");
	  Work_Card.Btn_Show(driver).click();
	  WebDriverWait wait1 = new WebDriverWait(driver,50);
	  wait1.until(ExpectedConditions.visibilityOf(Work_Card.Cancel_Sidur(driver)));
	  Work_Card.Cancel_Sidur(driver).click();
      Work_Card.Btn_Update(driver).click();
      //Thread.sleep(2000);
      Work_Card.Wait_For_Element_Stalenes(driver, "clnDate");
      Work_Card.Date(driver).click();
	  Work_Card.Date(driver).sendKeys("28042015");
	  Work_Card.Btn_Show(driver).click();
	  Work_Card.Btn_Add_Absence(driver).click();
	  Utils f= new Utils();
	  f.waitForWindow("DivuachHeadrut",driver);
	  Select droplist2 = new Select(Divuach_Headrut.List_Absences(driver));
      droplist2.selectByVisibleText("������� - ����� ��� �����"); 
      Divuach_Headrut.Btn_Update_Absence(driver).click();
      WebDriverWait wait2 = new WebDriverWait(driver, 30);
      wait2.until(ExpectedConditions.alertIsPresent());
	  Alert alert=driver.switchTo().alert();
	  System.out.println(alert.getText());
	  Assert.assertEquals("����� �������� ���� ����� �� ����� ����",alert.getText());
	  alert.accept();
	  Divuach_Headrut.Close_Add_Absences(driver).click();
	  Utils g= new Utils();
	  g.waitForWindow("WorkCard",driver);
      Work_Card.Btn_Close(driver).click();
      
     
	  
	  
	  
	  
	  
	  
  }
  
  
  
  
  
  
  








@BeforeMethod
  public void beforeMethod() {
	driver=getDriver();
	//  driver=Base.Initialize_browser();
	//  Base.Initialize_Webpage(driver);
	Utils.Enter_Workcard(driver);
	  
	  
	  
  }

  
  
  /*
  
  @AfterMethod
  public void afterMethod() {
	  
	  driver.quit();
	  
  }*/

}