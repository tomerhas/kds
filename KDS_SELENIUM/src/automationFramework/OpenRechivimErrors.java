 package automationFramework;

import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.Keys;
import org.openqa.selenium.WebDriver;
import org.testng.Assert;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import pageObjects.WorkCard;
import utils.Base;
import utils.Utilsfn;

public class OpenRechivimErrors extends Base {

	public WebDriver driver;

	
	  @Test (priority=0) public void openRechivim() {
	  
		Utilsfn a = new Utilsfn();
		a.waitForWindow("WorkCard", driver);
		WorkCard.TxtId(driver).sendKeys(Keys.TAB);
		WorkCard.Date(driver).sendKeys("26012017");
		WorkCard.Btn_Show(driver).click();
		WorkCard.Wait_For_Element_Stalenes(driver, "btnCalcItem", null);
		WorkCard.BtnCalcItems(driver).click();
		Utilsfn b = new Utilsfn();
		b.waitForWindow("Rechivim", driver);
		System.out.println(driver.getCurrentUrl());
		//to do : change to kdstest
		Assert.assertEquals(driver.getCurrentUrl(),
				"http://kdsshaldor/Modules/Ovdim/Rechivim.aspx?id=31777&date=26/01/2017");
		//JavascriptExecutor executor = (JavascriptExecutor) driver;
		//executor.executeScript("arguments[0].click();",
				//Work_Card.BtnCloseErrors(driver));
		driver.close();
		Utilsfn c = new Utilsfn();
		c.waitForWindow("WorkCard", driver);
		WorkCard.Btn_Close(driver).click();
	    
	  
	  
	  }
	 

	@Test  (priority=1)
	public void openErrors() {

		Utilsfn a = new Utilsfn();
		a.waitForWindow("WorkCard", driver);
		WorkCard.TxtId(driver).sendKeys(Keys.TAB);
		WorkCard.Date(driver).sendKeys("26012017");
		WorkCard.Btn_Show(driver).click();
		WorkCard.Wait_For_Element_Stalenes(driver, "btnDrvErrors", null);
		WorkCard.BtnDriverErrors(driver).click();
		Utilsfn b = new Utilsfn();
		b.waitForWindow("Errors", driver);
		String result = driver.getCurrentUrl().substring(0,
				driver.getCurrentUrl().length() - 68);
		System.out.println(result);
		//to do : change to kdstest
		Assert.assertEquals(result,
				"http://kdsshaldor/Modules/Ovdim/WorkCardErrors.aspx?");
		

		JavascriptExecutor executor = (JavascriptExecutor) driver;
		executor.executeScript("arguments[0].click();",
				WorkCard.BtnCloseErrors(driver));
		Utilsfn c = new Utilsfn();
		c.waitForWindow("WorkCard", driver);
		WorkCard.Btn_Close(driver).click();

	}

	@BeforeMethod
	public void beforeMethod() {

		driver = getDriver();
		Utilsfn.Enter_Workcard(driver);

	}

}
