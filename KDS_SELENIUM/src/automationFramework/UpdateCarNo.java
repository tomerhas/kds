package automationFramework;

import java.util.concurrent.TimeUnit;

import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.WebDriver;
import org.testng.Assert;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Listeners;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import pageObjects.WorkCard;
import utils.Base;
import utils.Utilsfn;




@Listeners ({Listener.TestListener.class})
public class UpdateCarNo  extends Base {
	
	public WebDriver driver;	
	 
	
	
	 @DataProvider(name = "Param_Car")
	  public Object[][] Parameters_Car_No() {
	          return new Object[][] {
	              {"8079614"},
	              { "8777901"},
	              { "7620669"}
	            
	            
	           };
	                    
	          };
	
	
  @Test (dataProvider="Param_Car")
  
  public void updateCarNo ( String sCar_No  )   throws InterruptedException {
	  
         
	   /*  waitForNumberofWindowsToEqual(2);
	     Set<String> handles = driver.getWindowHandles();
	     System.out.println(handles);
	     String firstWinHandle = driver.getWindowHandle(); handles.remove(firstWinHandle);
	     
	     String winHandle=(String) handles.iterator().next();
	     if (winHandle!=firstWinHandle){
	    	 String secondWinHandle = winHandle;
	    	 driver.switchTo().window(secondWinHandle);
	    	 System.out.println(secondWinHandle);}*/
	     
	     //Thread.sleep(3000);
	      //for (String handle : driver.getWindowHandles()) {
	      //driver.switchTo().window(handle);}
	      
	     
	      //driver.switchTo().window("window");
	     
	      
		  driver.manage().timeouts().implicitlyWait(30, TimeUnit.SECONDS);
		 
		 // WebDriverWait wait = new WebDriverWait(driver, 150);
		  //wait.until(ExpectedConditions.textToBePresentInElementValue(Work_Card.TxtId(driver), "31777"));
		  //Thread.sleep(3000);
		  
		 

		  Utilsfn a=new Utilsfn();
		  a.waitForWindow("WorkCard",driver);
		  
		  //Work_Card.findElement(driver, By.id("txtId"),60).sendKeys("77104");
		  WorkCard.TxtId(driver).sendKeys("77104");
		  WorkCard.Date(driver).clear();
		  WorkCard.Date(driver).sendKeys("04092016");
		  WorkCard.Btn_Show(driver).click();
		  //Thread.sleep(3000);	
		  WorkCard.Wait_For_Element_Visibile(driver, 60, "SD_000_ctl03_SD_000_ctl03LicenseNumber");
		  WorkCard.Car_Num(driver).click();
	      WorkCard.Car_Num(driver).sendKeys(sCar_No);
	      if (sCar_No=="8079614") {
	      Assert.assertEquals(WorkCard.Validate_Popup(driver).getText(),"מספר רישוי שגוי");
	      
	      WorkCard.Btn_Close(driver).click();
	      
	      //wait.until(ExpectedConditions.elementToBeClickable(Work_Card.Btn_Cancel_Update(driver)));
	      //WebElement element = driver.findElement(By.id("btnCancel"));
	      JavascriptExecutor js = (JavascriptExecutor)driver; 
	      js.executeScript("arguments[0].click();", WorkCard.Btn_Cancel_Update(driver)); 
	      //Work_Card.Btn_Cancel_Update(driver).click();
	      }
	      
	      else {
	      WorkCard.Btn_No_Copy_Car_Num(driver).click();
	      System.out.println(WorkCard.Assert_Car_Num(driver).getAttribute("value"));
	      if (sCar_No=="8777901"){
	      Assert.assertEquals(WorkCard.Assert_Car_Num(driver).getAttribute("value"),"7620669");
	      }
	      else {
	      Assert.assertEquals(WorkCard.Assert_Car_Num(driver).getAttribute("value"),"8777901");	
	      }
	      WorkCard.Car_Num(driver).click();
	      WorkCard.Car_Num(driver).sendKeys(sCar_No);
	      WorkCard.Btn_Yes_Copy_Car_Num(driver).click();
	      System.out.println(WorkCard.Assert_Car_Num(driver).getAttribute("value"));
	      if (sCar_No=="8777901")
	      Assert.assertEquals(WorkCard.Assert_Car_Num(driver).getAttribute("value"),"8777901");
	      else {
	      Assert.assertEquals(WorkCard.Assert_Car_Num(driver).getAttribute("value"),"7620669");
          }
	      WorkCard.Btn_Update(driver).click();
	      //Thread.sleep(3000);
	      
	      //Work_Card.Btn_Close(driver).click();
	      WorkCard.Wait_For_Element_Stalenes(driver,"btnCloseCard",null);
	      WorkCard.Btn_Close(driver).click();
	      }
	      
	      
	      
	  	  
  }
  
  
  
  
  
  @BeforeMethod
  public void beforeMethod()  {
	  
	  
	  driver=getDriver();
	  Utilsfn.Enter_Workcard(driver);
	  
	  
  }

  
  
    
  

   
}
