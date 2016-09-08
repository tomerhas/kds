package automationFramework;

import java.util.List;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;

import utils.Base;
import utils.Utilsfn;

public class NewTest extends Base {
	
	public  WebDriver driver;
	
	
	
  @Test
  public void f() {
	  
	  
	  
	  
	  Utilsfn a= new Utilsfn();
      a.Click_Sub_Menu("הפעלת מהלכים", "הרצת חישוב",driver);
      driver.manage().window().maximize();
      
      List<WebElement> checkboxes = driver.findElements(By.cssSelector("input[type=\"checkbox\"]"));

    
      System.out.println("With .attribute('checked')");
      for (WebElement checkbox : checkboxes) {
          System.out.println(String.valueOf(checkbox.getAttribute("checked")));
      }

      System.out.println("\nWith .selected?");
      for (WebElement checkbox : checkboxes) {
          System.out.println(checkbox.isSelected());
          
      }
 
	  
	  
	  
	  
	  
	  
	  
	  
  }
  
  @BeforeMethod
  public void beforeMethod() {
	  
	  driver=getDriver();
	  
	  
	  
  }
  
  
}
