package automationFramework;

import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.Keys;
import org.openqa.selenium.WebDriver;
import org.testng.Assert;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.AfterMethod;

import pageObjects.Work_Card;
import utils.Base;
import utils.Utils;

public class Test_open_Rechivim_Errors extends Base {

	public WebDriver driver;

	
	  @Test (priority=0) public void open_Rechivim() {
	  
		Utils a = new Utils();
		a.waitForWindow("WorkCard", driver);
		Work_Card.TxtId(driver).sendKeys(Keys.TAB);
		Work_Card.Date(driver).sendKeys("27102015");
		Work_Card.Btn_Show(driver).click();
		Work_Card.Wait_For_Element_Stalenes(driver, "btnCalcItem", null);
		Work_Card.BtnCalcItems(driver).click();
		Utils b = new Utils();
		b.waitForWindow("Rechivim", driver);
		System.out.println(driver.getCurrentUrl());
		Assert.assertEquals(driver.getCurrentUrl(),
				"http://kdstest/Modules/Ovdim/Rechivim.aspx?id=31777&date=27/10/2015");
		//JavascriptExecutor executor = (JavascriptExecutor) driver;
		//executor.executeScript("arguments[0].click();",
				//Work_Card.BtnCloseErrors(driver));
		driver.close();
		Utils c = new Utils();
		c.waitForWindow("WorkCard", driver);
		Work_Card.Btn_Close(driver).click();
	    
	  
	  
	  }
	 

	@Test  (priority=1)
	public void open_Errors() {

		Utils a = new Utils();
		a.waitForWindow("WorkCard", driver);
		Work_Card.TxtId(driver).sendKeys(Keys.TAB);
		Work_Card.Date(driver).sendKeys("27102015");
		Work_Card.Btn_Show(driver).click();
		Work_Card.Wait_For_Element_Stalenes(driver, "btnDrvErrors", null);
		Work_Card.BtnDriverErrors(driver).click();
		Utils b = new Utils();
		b.waitForWindow("Errors", driver);
		String result = driver.getCurrentUrl().substring(0,
				driver.getCurrentUrl().length() - 68);
		System.out.println(result);
		Assert.assertEquals(result,
				"http://kdstest/Modules/Ovdim/WorkCardErrors.aspx?");
		

		JavascriptExecutor executor = (JavascriptExecutor) driver;
		executor.executeScript("arguments[0].click();",
				Work_Card.BtnCloseErrors(driver));
		Utils c = new Utils();
		c.waitForWindow("WorkCard", driver);
		Work_Card.Btn_Close(driver).click();

	}

	@BeforeMethod
	public void beforeMethod() {

		driver = getDriver();
		Utils.Enter_Workcard(driver);

	}

}
