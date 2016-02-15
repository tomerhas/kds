 package automationFramework;

import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.Keys;
import org.openqa.selenium.WebDriver;
import org.testng.Assert;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.AfterMethod;

import pageObjects.WorkCard;
import utils.Base;
import utils.Utils;

public class OpenRechivimErrors extends Base {

	public WebDriver driver;

	
	  @Test (priority=0) public void openRechivim() {
	  
		Utils a = new Utils();
		a.waitForWindow("WorkCard", driver);
		WorkCard.TxtId(driver).sendKeys(Keys.TAB);
		WorkCard.Date(driver).sendKeys("27102015");
		WorkCard.Btn_Show(driver).click();
		WorkCard.Wait_For_Element_Stalenes(driver, "btnCalcItem", null);
		WorkCard.BtnCalcItems(driver).click();
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
		WorkCard.Btn_Close(driver).click();
	    
	  
	  
	  }
	 

	@Test  (priority=1)
	public void openErrors() {

		Utils a = new Utils();
		a.waitForWindow("WorkCard", driver);
		WorkCard.TxtId(driver).sendKeys(Keys.TAB);
		WorkCard.Date(driver).sendKeys("27102015");
		WorkCard.Btn_Show(driver).click();
		WorkCard.Wait_For_Element_Stalenes(driver, "btnDrvErrors", null);
		WorkCard.BtnDriverErrors(driver).click();
		Utils b = new Utils();
		b.waitForWindow("Errors", driver);
		String result = driver.getCurrentUrl().substring(0,
				driver.getCurrentUrl().length() - 68);
		System.out.println(result);
		Assert.assertEquals(result,
				"http://kdstest/Modules/Ovdim/WorkCardErrors.aspx?");
		

		JavascriptExecutor executor = (JavascriptExecutor) driver;
		executor.executeScript("arguments[0].click();",
				WorkCard.BtnCloseErrors(driver));
		Utils c = new Utils();
		c.waitForWindow("WorkCard", driver);
		WorkCard.Btn_Close(driver).click();

	}

	@BeforeMethod
	public void beforeMethod() {

		driver = getDriver();
		Utils.Enter_Workcard(driver);

	}

}
