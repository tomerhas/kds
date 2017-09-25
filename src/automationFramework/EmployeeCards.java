package automationFramework;


import java.util.concurrent.TimeUnit;

import org.junit.Assert;
import org.openqa.selenium.Alert;
import org.openqa.selenium.Keys;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Listeners;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import pageObjects.EmployeeCard;
import pageObjects.LogInPage;
import utils.Base;
import utils.Utilsfn;








@Listeners ({Listener.TestListener.class})
public class EmployeeCards  extends Base {
	public WebDriver driver;
	
	 @DataProvider(name = "Param_Id")
	  public Object[][] Parameters_Id() {
	          return new Object[][] {
	            { "74013","01/2017",null},
	        	{ "87903","01/2017",null},
	            { "77104","12/2016",null},
	            { null,"01/2017","אבו שמסיה ראאפת"},
	            { null,"12/2016", "צפורי אלון"}
	           };
	                    
	          };

	
	

	      
	
 @Test(priority=0, dataProvider = "Param_Id")

 
  
  public void  chkEmployeeCards (String sMispar_ishi ,String sdate, String sName  ) {
	
	 
      LogInPage.lnk_EmployeeCards(driver).click();
      driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
      Assert.assertTrue("The radio button Rdo_Id is not selected ",EmployeeCard.Rdo_Id(driver).isSelected());
      if (sMispar_ishi!=null) 
      {
    	  EmployeeCard.Txt_Id(driver).sendKeys(sMispar_ishi);
    	  EmployeeCard.Txt_Id(driver).sendKeys(Keys.ENTER);
  
      } 
    	  
      if  (sMispar_ishi=="74013")
      {
          WebDriverWait wait = new WebDriverWait(driver, 30);
    	  wait.until(ExpectedConditions.alertIsPresent());
          Alert alert=driver.switchTo().alert();
          System.out.println("Alert is present");
          alert.getText();
          Assert.assertEquals("מספר אישי לא קיים/אינך מורשה לצפות בעובד זה", alert.getText());
          System.out.println(alert.getText());
          alert.accept();
      }
      Assert.assertTrue("The radio button Rdo_Month is not selected ",EmployeeCard.Rdo_Month(driver).isSelected());
      Select droplist = new Select(EmployeeCard.List_Month(driver));
      droplist.selectByVisibleText(sdate); 
	  
	  if (sName!=null)
	  {	      
	        EmployeeCard.Rdo_Name(driver).click();
	        Assert.assertFalse("The radio button Rdo_Id is selected ",EmployeeCard.Rdo_Id(driver).isSelected());
	  }
	  if (sName!=null )
	  {
		  EmployeeCard.Txt_name(driver).click();
	      EmployeeCard.Txt_name(driver).sendKeys(sName);
	      EmployeeCard.List_Month(driver).sendKeys(Keys.ARROW_DOWN);
	      EmployeeCard.List_Month(driver).sendKeys(Keys.ENTER);
	  }
	  
	 
      droplist.selectByVisibleText(sdate);
     
      
      
      EmployeeCard.Btn_Execute(driver).click();
     
    if ((sMispar_ishi!="74013"))
      Assert.assertTrue("The grid Grd_Employee is not displayed", EmployeeCard.Grd_Employee(driver).isDisplayed());
       
  }
  
  

  
  
 @BeforeMethod
  public void beforeMethod()  {
	  
	  driver=getDriver();
	  Utilsfn.Initialize_Webpage(driver);
	  
	  
	  
  }

  
  
  
  
}
