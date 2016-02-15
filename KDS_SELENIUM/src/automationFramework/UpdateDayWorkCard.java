package automationFramework;

import java.util.concurrent.TimeUnit;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.Select;
import org.testng.Assert;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Listeners;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.AfterMethod;

import pageObjects.Work_Card;
import utils.Base;
import utils.Utils;

import java.io.IOException;
import java.lang.Thread;




@Listeners ({Listener.TestListener.class})
public class UpdateDayWorkCard  extends Base  {

	public WebDriver driver;
	public WebElement element;
	
	 @DataProvider(name = "Param_dd")
	  public Object[][] Parameters_dd() {
	          return new Object[][] {
	            { "חסר טכוגרף","לינה אחת"},
	        	{ "צירף טכוגרף","אין לינה"}
	           
	           };
	                    
	          };
	
	
	          
	        
	
	
	
  @Test  
 ( dataProvider = "Param_dd") 
  






public void updateDayWorkCard   (String sTachograph,String sLina ) throws InterruptedException, IOException {


	  driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	  Utils a=new Utils();
	  a.waitForWindow("WorkCard",driver);
	  Work_Card.TxtId(driver).sendKeys("77104");
	  Work_Card.Date(driver).click();
	  Work_Card.Date(driver).sendKeys("03032015");
	  Work_Card.Btn_Show(driver).click();
	  //Thread.sleep(3000);
	  Work_Card.Wait_For_Element_Stalenes(driver, "btnPlus2",null);
	  Work_Card.Day_Plus(driver).click();
	  Select droplist = new Select(Work_Card.Tachograph(driver));
      droplist.selectByVisibleText(sTachograph); 
      Select droplist1 = new Select(Work_Card.Lina(driver));
      droplist1.selectByVisibleText(sLina); 
      Assert.assertFalse(Work_Card.Hamara(driver).isEnabled(),"The Checkbox Hamara is Enabled");
      Assert.assertFalse(Work_Card.Halbasha(driver).isEnabled(),"The Checkbox Halbasha is Enabled");
      Assert.assertFalse(Work_Card.HashlamaForDay(driver).isEnabled(),"The Checkbox HashlamaForDay is Enabled");
      Assert.assertFalse(Work_Card.HashlamaReason(driver).isEnabled(),"The Checkbox HashlamaReason is Enabled");
      Work_Card.Btn_Update(driver).click();
      //Thread.sleep(3000);
      Work_Card.Wait_For_Element_Stalenes(driver, "btnCloseCard",null);
      Work_Card.Btn_Close(driver).click();
     // driver.close();
}



	  
  
  
  
  
  

  





@BeforeMethod
  public void beforeMethod() throws IOException {

      
	  driver=getDriver();
	  Utils.Enter_Workcard(driver);
	  
      
      
  }


     
}
