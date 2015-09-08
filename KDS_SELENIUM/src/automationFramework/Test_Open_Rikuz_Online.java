package automationFramework;

import org.openqa.selenium.By;
import org.openqa.selenium.Keys;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.interactions.Actions;
import org.openqa.selenium.support.ui.Select;
import org.testng.annotations.Listeners;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;

import pageObjects.Employee_Card;
import pageObjects.Tickur_Chishuv_LeOved;
import pageObjects.Work_Card;
import utils.Base;
import utils.Utils;





@Listeners ({Listener.TestListener.class})
public class Test_Open_Rikuz_Online extends Base {
	
	public  WebDriver driver;
	
	
	
  @Test
  public void Open_Rikuz_Online() {
	  
	  
	
	  
      
      Utils a= new Utils();
      a.Click_Sub_Menu("חישוב לעובד", "חישוב לעובד מקוון",driver);
      driver.manage().window().maximize();
      System.out.println(driver.getCurrentUrl());
      System.out.println(driver.getWindowHandles());
      Tickur_Chishuv_LeOved.Mispar_Ishi(driver).click();
      Tickur_Chishuv_LeOved.Mispar_Ishi(driver).sendKeys("31777");
      Tickur_Chishuv_LeOved.Mispar_Ishi(driver).sendKeys(Keys.TAB);
      Work_Card.Wait_For_Element_Visibile(driver, 60,"ctl00_KdsContent_ddlMonth");
      Select droplist = new Select(Tickur_Chishuv_LeOved.List_Month(driver));
      droplist.selectByVisibleText("07/2015");
      Tickur_Chishuv_LeOved.List_Month(driver).sendKeys(Keys.ENTER);
      Tickur_Chishuv_LeOved.Btn_Show(driver).click();
	  Work_Card.Wait_For_Element_Stalenes(driver,null, "ItemRow");
	  Tickur_Chishuv_LeOved.Btn_Print(driver).click();
	  Utils b= new Utils();
	  b.waitForWindow("ModalShowPrint",driver);
	  driver .close();
      
      
      
      
	  
	  
  }
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  @BeforeMethod
  public void beforeMethod() {
	  
	  driver=getDriver();
	  
	  
	  
  }

}
