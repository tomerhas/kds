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

import pageObjects.DivuachHeadrut;
import pageObjects.WorkCard;
import utils.Utilsfn;
import utils.Base;




@Listeners ({Listener.TestListener.class})
public class AddAbsences    extends Base {
	
	public  WebDriver driver;
	
	
	
  @Test
  public void addAbsences() throws InterruptedException {
	  
	  
	  
	  
driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	  
	  
	  Utilsfn a= new Utilsfn();
	  a.waitForWindow("WorkCard",driver);
	  WorkCard.TxtId(driver).sendKeys("85400");
	  WorkCard.Date(driver).click();
	  WorkCard.Date(driver).sendKeys("26042015");
	  WorkCard.Btn_Show(driver).click();
	  //Thread.sleep(2000);
	  //Work_Card.Wait_For_Element_Visibile(driver,60,"btnAddHeadrut");
	  WorkCard.Wait_For_Element_Stalenes(driver, "btnAddHeadrut",null);
	  WorkCard.Btn_Add_Absence(driver).click();
	  Utilsfn b= new Utilsfn();
	  b.waitForWindow("DivuachHeadrut",driver);
	  Select droplist = new Select(DivuachHeadrut.List_Absences(driver));
      droplist.selectByVisibleText("היעדרות - תשלום יום עבודה"); 
      DivuachHeadrut.Start_Time_Absences(driver).sendKeys("0600");
      DivuachHeadrut.End_Time_Absences(driver).click();
      DivuachHeadrut.End_Time_Absences(driver).sendKeys("2300");
      DivuachHeadrut.Btn_Update_Absence(driver).click();
      Utilsfn c= new Utilsfn();
	  c.waitForWindow("WorkCard",driver);
	  System.out.println(driver.getCurrentUrl());
      Assert.assertEquals(WorkCard.Lbl_Sidur_Num_0(driver).getText(),"99801");
	  WorkCard.Cancel_Sidur(driver).click();
      WorkCard.Btn_Update(driver).click();
      WorkCard.Wait_For_Element_Stalenes(driver, "btnAddHeadrut",null);
      WorkCard.Btn_Add_Absence(driver).click();
      Utilsfn d= new Utilsfn();
	  d.waitForWindow("DivuachHeadrut",driver);
	  Select droplist1 = new Select(DivuachHeadrut.List_Absences(driver));
      droplist1.selectByVisibleText("מילואים"); 
      DivuachHeadrut.End_Date_Absences(driver).click();
      DivuachHeadrut.End_Date_Absences(driver).sendKeys("27042015");
      DivuachHeadrut.Btn_Update_Absence(driver).click();
      Utilsfn e= new Utilsfn();
	  e.waitForWindow("WorkCard",driver);
      Assert.assertEquals(WorkCard.Lbl_Sidur_Num_0(driver).getText(),"99830");
      WorkCard.Date(driver).click();
	  WorkCard.Date(driver).sendKeys("27042015");
	  WorkCard.Btn_Show(driver).click();
	  //Thread.sleep(2000);
	  WorkCard.Wait_For_Element_Visibile(driver,60,"SD_lblSidur0");
	  //WebDriverWait wait = new WebDriverWait(driver,50);
	  //wait.until(ExpectedConditions.visibilityOf(Work_Card.Lbl_Sidur_Num_0(driver)));
	  Assert.assertEquals(WorkCard.Lbl_Sidur_Num_0(driver).getText(),"99830");
	  WorkCard.Cancel_Sidur(driver).click();
      WorkCard.Btn_Update(driver).click();
      //Thread.sleep(2000);
      WorkCard.Wait_For_Element_Stalenes(driver, "clnDate",null);
      WorkCard.Date(driver).click();
	  WorkCard.Date(driver).sendKeys("26042015");
	  WorkCard.Btn_Show(driver).click();
	  WebDriverWait wait1 = new WebDriverWait(driver,50);
	  wait1.until(ExpectedConditions.visibilityOf(WorkCard.Cancel_Sidur(driver)));
	  WorkCard.Cancel_Sidur(driver).click();
      WorkCard.Btn_Update(driver).click();
      //Thread.sleep(2000);
      WorkCard.Wait_For_Element_Stalenes(driver, "clnDate",null);
      WorkCard.Date(driver).click();
	  WorkCard.Date(driver).sendKeys("28042015");
	  WorkCard.Btn_Show(driver).click();
	  WorkCard.Btn_Add_Absence(driver).click();
	  Utilsfn f= new Utilsfn();
	  f.waitForWindow("DivuachHeadrut",driver);
	  Select droplist2 = new Select(DivuachHeadrut.List_Absences(driver));
      droplist2.selectByVisibleText("היעדרות - תשלום יום עבודה"); 
      DivuachHeadrut.Btn_Update_Absence(driver).click();
      WebDriverWait wait2 = new WebDriverWait(driver, 30);
      wait2.until(ExpectedConditions.alertIsPresent());
	  Alert alert=driver.switchTo().alert();
	  System.out.println(alert.getText());
	  Assert.assertEquals("סידור ההיעדרות חופף בשעות עם סידור קיים",alert.getText());
	  alert.accept();
	  DivuachHeadrut.Close_Add_Absences(driver).click();
	  Utilsfn g= new Utilsfn();
	  g.waitForWindow("WorkCard",driver);
      WorkCard.Btn_Close(driver).click();
      
     
	  
	  
	  
	  
	  
	  
  }
  
  
  
  
  
  
  








@BeforeMethod
  public void beforeMethod() {
	driver=getDriver();
	//  driver=Base.Initialize_browser();
	//  Base.Initialize_Webpage(driver);
	Utilsfn.Enter_Workcard(driver);
	  
	  
	  
  }

  
  
  /*
  
  @AfterMethod
  public void afterMethod() {
	  
	  driver.quit();
	  
  }*/

}
