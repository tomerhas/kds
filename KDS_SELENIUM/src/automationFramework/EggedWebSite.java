package automationFramework;

import java.util.concurrent.TimeUnit;

import org.openqa.selenium.By;
import org.openqa.selenium.Keys;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.Assert;
import org.testng.annotations.Test;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.AfterMethod;

import pageObjects.EmployeeCard;
import pageObjects.WorkCard;
import utils.Utilsfn;

public class EggedWebSite {
	
	 
	public  WebDriver driver;
	
	
	
  @Test
  public void f() {
	  driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
	  driver.getCurrentUrl();
	  System.out.println(driver.getWindowHandles());
	  driver.findElement(By.linkText("חיפוש לפי מוצא ויעד")).click();
	  System.out.println(driver.findElement(By.linkText("חיפוש לפי מוצא ויעד")).getText());
      //for (String handle : driver.getWindowHandles()) {
      //driver.switchTo().window(handle);}
	  Utilsfn a =new Utilsfn();
	  a.waitForWindow("origindestination", driver);
	  System.out.println(driver.getCurrentUrl());
	  //System.out.println(driver.getWindowHandles());
	  //driver.findElement(By.cssSelector("body")).sendKeys(Keys.CONTROL, Keys.PAGE_DOWN);
	  System.out.println(driver.getCurrentUrl());
	  WebDriverWait wait = new WebDriverWait(driver, 50);
	 wait.until(ExpectedConditions.visibilityOf(driver
				.findElement(By.xpath("//div[@class='AddressSearchOpenIcon']"))));
	  driver.findElement(By.xpath("//div[@class='AddressSearchOpenIcon']")).click();
	  //driver.findElement(By.className("AddressSearchOpenIcon")).click();
	
      driver.findElement(By.id("bydestFromCityCombo")).sendKeys("אסמ"); 
      driver.findElement(By.xpath("//div[contains(@class,'ng-binding') and contains(text(),'אסמאעיל')]")).click();
      //System.out.println(driver.findElement(By.id("ui-id-4")).getText());
      Assert.assertEquals(driver.findElement(By.id("ui-id-3")).getText(), "אגד אינה מספקת שירות לישוב אסמאעיל");
      //Assert.assertEquals(By.xpath("//div[contains(@class,'ng-binding') and contains(text(),'אסמאעיל')]"), "אגד אינה מספקת שירות לישוב אסמאעיל");
	  
      
      
	  
  }
  
  
  
  @BeforeMethod
  public void beforeMethod() {
	  System.setProperty("webdriver.chrome.driver", "/selenium/chromedriver.exe");
	  driver = new ChromeDriver();
	  driver.get("http://www.egged.co.il/");
	  driver.manage().window().maximize();
	  
	  
	  
  }

  
  
  
  
  
  
  
  
  @AfterMethod
  public void afterMethod() {
	  
	  driver.quit();
	  
  }

}
