package automationFramework;

import java.util.concurrent.TimeUnit;

import org.openqa.selenium.By;
import org.openqa.selenium.Keys;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Listeners;
import org.testng.annotations.Test;

import pageObjects.Tickur_Chishuv_LeOved;
import pageObjects.Work_Card;
import utils.Base;
import utils.Utils;





@Listeners ({Listener.TestListener.class})
public class Test_Open_Rikuz_Online extends Base {
	
	public  WebDriver driver;
	
	
	
  @Test
  public void Open_Rikuz_Online() {
	  
	  
	  driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	  
      
      Utils a= new Utils();
      a.Click_Sub_Menu("חישוב לעובד", "חישוב לעובד מקוון",driver);
      driver.manage().window().maximize();
      
     
      
      Tickur_Chishuv_LeOved.Mispar_Ishi(driver).click();
      Tickur_Chishuv_LeOved.Mispar_Ishi(driver).sendKeys("31777");
      Tickur_Chishuv_LeOved.Mispar_Ishi(driver).sendKeys(Keys.TAB);
      WebDriverWait wait = new WebDriverWait(driver,20);

      wait.until(ExpectedConditions.visibilityOfElementLocated(By.id("ctl00_KdsContent_ddlMonth")));
      String text = Tickur_Chishuv_LeOved.List_Month(driver).getText();
      System.out.println(text);
     
     
      
     // Work_Card.Wait_For_Element_Stalenes(driver, "ctl00_KdsContent_ddlMonth", null);
      //Work_Card.Wait_For_Element_Visibile(driver, 60,"ctl00_KdsContent_ddlMonth");
      
   
      
      Select droplist = new Select(Tickur_Chishuv_LeOved.List_Month(driver));
      
      String text1 = Tickur_Chishuv_LeOved.List_Month(driver).getText();
      System.out.println(text1);
      
      droplist.selectByVisibleText("07/2015");
      Tickur_Chishuv_LeOved.List_Month(driver).sendKeys(Keys.ENTER);
      /*Tickur_Chishuv_LeOved.Btn_Show(driver).click();
	  Work_Card.Wait_For_Element_Stalenes(driver,null, "ItemRow");
	  Tickur_Chishuv_LeOved.Btn_Print(driver).click();
	  Utils b= new Utils();
	  b.waitForWindow("ModalShowPrint",driver);
	  System.out.println(driver.getCurrentUrl());
	  Assert.assertEquals(driver.getCurrentUrl(), "http://kdstest/ModalShowPrint.aspx", "window doesn't exist" );*/
	  driver .close();
      
      
      
      
	  
	  
  }
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  @BeforeMethod
  public void beforeMethod() {
	  
	  driver=getDriver();
	  
	  
	  
  }

}
