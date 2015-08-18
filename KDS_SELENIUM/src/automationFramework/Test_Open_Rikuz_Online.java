package automationFramework;

import org.openqa.selenium.By;
import org.openqa.selenium.Keys;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.interactions.Actions;
import org.testng.annotations.Listeners;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;

import pageObjects.Tickur_Chishuv_LeOved;
import utils.Base;
import utils.Utils;





@Listeners ({Listener.TestListener.class})
public class Test_Open_Rikuz_Online extends Base {
	
	public  WebDriver driver;
	
	
	
  @Test
  public void Open_Rikuz_Online() {
	  
	  
	
	  
      
      Utils a= new Utils();
      a.Click_Sub_Menu("חישוב לעובד", "חישוב לעובד מקוון",driver);
      System.out.println(driver.getCurrentUrl());
      System.out.println(driver.getWindowHandles());
      Tickur_Chishuv_LeOved.Mispar_Ishi(driver).click();
      Tickur_Chishuv_LeOved.Mispar_Ishi(driver).sendKeys("31777");
      Tickur_Chishuv_LeOved.Mispar_Ishi(driver).sendKeys(Keys.TAB);
      Tickur_Chishuv_LeOved.Btn_Show(driver).click();
      
	  
	  
      
      
      
      
	  
	  
  }
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  @BeforeMethod
  public void beforeMethod() {
	  
	  driver=getDriver();
	  
	  
	  
  }

}
