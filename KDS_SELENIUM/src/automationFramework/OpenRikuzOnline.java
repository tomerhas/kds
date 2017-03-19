package automationFramework;

import java.util.concurrent.TimeUnit;

import org.openqa.selenium.By;
import org.openqa.selenium.Keys;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.Assert;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Listeners;
import org.testng.annotations.Test;

import pageObjects.TickurChishuvLeOved;
import pageObjects.WorkCard;
import utils.Base;
import utils.Utilsfn;





@Listeners ({Listener.TestListener.class})
public class OpenRikuzOnline extends Base {
	
	public  WebDriver driver;
	
	
	
  @Test
  public void openRikuzOnline() {
	  
	  
	  driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	  
      
      Utilsfn a= new Utilsfn();
      a.Click_Sub_Menu("חישוב לעובד", "חישוב לעובד מקוון",driver);
      driver.manage().window().maximize();
      
     
      
      TickurChishuvLeOved.Mispar_Ishi(driver).click();
      TickurChishuvLeOved.Mispar_Ishi(driver).sendKeys("31777");
      TickurChishuvLeOved.Mispar_Ishi(driver).sendKeys(Keys.TAB);
      WebDriverWait wait = new WebDriverWait(driver,20);

      wait.until(ExpectedConditions.visibilityOfElementLocated(By.id("ctl00_KdsContent_ddlMonth")));
      String text = TickurChishuvLeOved.List_Month(driver).getText();
      System.out.println(text);
     
     
      
     // Work_Card.Wait_For_Element_Stalenes(driver, "ctl00_KdsContent_ddlMonth", null);
      //Work_Card.Wait_For_Element_Visibile(driver, 60,"ctl00_KdsContent_ddlMonth");
      
   
      
      Select droplist = new Select(TickurChishuvLeOved.List_Month(driver));
      
      String text1 = TickurChishuvLeOved.List_Month(driver).getText();
      System.out.println(text1);
      
      droplist.selectByVisibleText("01/2017");
      TickurChishuvLeOved.List_Month(driver).sendKeys(Keys.ENTER);
      TickurChishuvLeOved.Btn_Show(driver).click();
	  WorkCard.Wait_For_Element_Stalenes(driver,null, "ItemRow");
	  TickurChishuvLeOved.Btn_Print(driver).click();
	  Utilsfn b= new Utilsfn();
	  b.waitForWindow("ModalShowPrint",driver);
	  System.out.println(driver.getCurrentUrl());
	  //to do : change to kdstest
	  Assert.assertEquals(driver.getCurrentUrl(), "http://kdsshaldor/ModalShowPrint.aspx", "window doesn't exist" );
	  driver .close();
      
      
      
      
	  
	  
  }
  
  
  
  
  
  
  
 
  
  
  
  
  @BeforeMethod
  public void beforeMethod() {
	  
	  driver=getDriver();
	  
	  
	  
  }

}
