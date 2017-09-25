package automationFramework;

import java.util.concurrent.TimeUnit;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.Select;
import org.testng.Assert;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Listeners;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import pageObjects.WorkCard;
import utils.Base;
import utils.Utilsfn;

import java.io.IOException;




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
	  Utilsfn a=new Utilsfn();
	  a.waitForWindow("WorkCard",driver);
	  WorkCard.TxtId(driver).sendKeys("77104");
	  WorkCard.Date(driver).click();
	  WorkCard.Date(driver).sendKeys("03112016");
	  WorkCard.Btn_Show(driver).click();
	  //Thread.sleep(3000);
	  WorkCard.Wait_For_Element_Stalenes(driver, "btnPlus2",null);
	  WorkCard.Day_Plus(driver).click();
	  Select droplist = new Select(WorkCard.Tachograph(driver));
      droplist.selectByVisibleText(sTachograph); 
      Select droplist1 = new Select(WorkCard.Lina(driver));
      droplist1.selectByVisibleText(sLina); 
      Assert.assertFalse(WorkCard.Hamara(driver).isEnabled(),"The Checkbox Hamara is Enabled");
      Assert.assertFalse(WorkCard.Halbasha(driver).isEnabled(),"The Checkbox Halbasha is Enabled");
      Assert.assertFalse(WorkCard.HashlamaForDay(driver).isEnabled(),"The Checkbox HashlamaForDay is Enabled");
      Assert.assertFalse(WorkCard.HashlamaReason(driver).isEnabled(),"The Checkbox HashlamaReason is Enabled");
      WorkCard.Btn_Update(driver).click();
      //Thread.sleep(3000);
      WorkCard.Wait_For_Element_Stalenes(driver, "btnCloseCard",null);
      WorkCard.Btn_Close(driver).click();
     // driver.close();
}



	  
  
  
  
  
  

  





@BeforeMethod
  public void beforeMethod() throws IOException {

      
	  driver=getDriver();
	  Utilsfn.Enter_Workcard(driver);
	  
      
      
  }


     
}
