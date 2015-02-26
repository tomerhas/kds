package automationFramework;

import java.io.File;
import java.util.concurrent.TimeUnit;

import org.junit.Assert;
import org.openqa.selenium.Alert;
import org.openqa.selenium.Keys;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.ie.InternetExplorerDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.AfterMethod;

import pageObjects.Employee_Card;
import pageObjects.LogIn_Page;

public class Test_EmployeeCards {
	public WebDriver driver;
	
	 @DataProvider(name = "Param_Id")
	  public Object[][] Parameters_Id() {
	          return new Object[][] {
	            { "74013","12/2014",null}
	        	//{ "87903","12/2014",null},
	            //{ "77104","02/2015",null},
	            //{ null,"12/2014","אבו שמסיה ראאפת"},
	            //{ null,"02/2015", "צפורי אלון"}
	           };
	                    
	          };

	
	

	
 @Test(priority=0, dataProvider = "Param_Id")

 
  
  public void f(String sMispar_ishi ,String sdate, String sName  ) {
	
	
      LogIn_Page.lnk_EmployeeCards(driver).click();
      driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
      Assert.assertTrue("The radio button Rdo_Id is not selected ",Employee_Card.Rdo_Id(driver).isSelected());
      if (sMispar_ishi!=null) 
      {
    	  Employee_Card.Txt_Id(driver).sendKeys(sMispar_ishi);
      }	  
      if (sMispar_ishi!="74013" && sMispar_ishi!=null )
    	  Employee_Card.Txt_Id(driver).sendKeys(Keys.ENTER);
    	  
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
      Assert.assertTrue("The radio button Rdo_Month is not selected ",Employee_Card.Rdo_Month(driver).isSelected());
      Select droplist = new Select(Employee_Card.List_Month(driver));
      droplist.selectByVisibleText(sdate); 
	  
	  if (sName!=null)
	  {	      
	        Employee_Card.Rdo_Name(driver).click();
	        Assert.assertFalse("The radio button is selected ",Employee_Card.Rdo_Id(driver).isSelected());
	  }
	  if (sName!=null )
	  {
		  Employee_Card.Txt_name(driver).click();
	      Employee_Card.Txt_name(driver).sendKeys(sName);
	  }
      droplist.selectByVisibleText(sdate);
      Employee_Card.Btn_Execute(driver).click();
       
  }
  
  

  
  
 @BeforeMethod
  public void beforeMethod() {
	  File file = new File("C:/Selenium/IEDriverServer.exe");
	  System.setProperty("webdriver.ie.driver", file.getAbsolutePath());
	  driver = new InternetExplorerDriver();
	  driver.navigate().to("http://kdstest");
	 
	  
	  
  }

  
  
  
  @AfterMethod
  public void afterMethod() {
	
	  
	  driver.close();
  }
  
}
