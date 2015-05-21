package automationFramework;

import java.util.concurrent.TimeUnit;

import org.openqa.selenium.ElementNotVisibleException;
import org.openqa.selenium.Keys;
import org.openqa.selenium.StaleElementReferenceException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.Assert;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.AfterMethod;

import pageObjects.Work_Card;

public class Test_Add_Activity {
	
	public  WebDriver driver;
	
	
	
  @Test
  public void f() {
	  
	  
	  
	  Base a =new Base();
	  a.waitForWindow("WorkCard",driver);
	  Work_Card.TxtId(driver).sendKeys("77104");
	  Work_Card.Date(driver).click();
	  Work_Card.Date(driver).sendKeys("17052015");
	  Work_Card.Btn_Show(driver).click();
	  Work_Card.find_Element(driver,60,"SD_imgAddPeilut0").click();
	  Work_Card.find_Element(driver,60,"SD_000_ctl08_SD_000_ctl08ShatYetiza").clear();
	  Work_Card.Entry_Time_Add_Activity(driver).sendKeys("1514");
	  Work_Card.Makat_Num_Add_Activity(driver).click();
	  Work_Card.Makat_Num_Add_Activity(driver).sendKeys("79100500");
	  Work_Card.Makat_Num_Add_Activity(driver).sendKeys(Keys.TAB);
	  System.out.println(Work_Card.Activity_Lable(driver).getText());
	  Assert.assertEquals(Work_Card.Activity_Lable(driver).getText(),"נסיעה ריקה שאינה בקטלוג");
	  
	  
	  
	  
	  
	  
	  
	  
	  
	  
	  
  }
  
  
  
  
  
  
  @BeforeMethod
  public void beforeMethod() {
	  
	  driver=Base.Initialize_browser();
	  Base.Initialize_Webpage(driver);
	  Base.Enter_Workcard(driver);
	  
	  
  }

  
  
  
  
  
  
  
  
  
  @AfterMethod
  public void afterMethod() {
	  
	  
	  driver.close();
  }

}
