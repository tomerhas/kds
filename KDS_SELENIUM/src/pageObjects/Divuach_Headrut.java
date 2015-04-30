package pageObjects;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

public class Divuach_Headrut {

	 private static WebElement element;

	
	 public static WebElement  Btn_Add_Absence (WebDriver driver){
		 
		  element = driver.findElement(By.id("btnAddHeadrut"));
		    
		 
		   return element;  }
	 
	 
	 
	 
	 
	 public static WebElement  Btn_Update_Absence (WebDriver driver){
		 
		  element = driver.findElement(By.id("btnUpdate"));
		    
		 
		   return element;  }
	 
	 
	 
	
	 public static WebElement  List_Absences (WebDriver driver){
		 
		  element = driver.findElement(By.id("ddlHeadrutType"));
		    
		 
		   return element;  }
	 
	 
	 
	 
	 
	 public static WebElement  Start_Time_Absences (WebDriver driver){
		 
		  element = driver.findElement(By.id("txtStartTime"));
		    
		 
		   return element;  }
	 
	 
	 
	 
	 public static WebElement  End_Time_Absences (WebDriver driver){
		 
		  element = driver.findElement(By.id("txtEndTime"));
		    
		 
		   return element;  }
	 
	 
	 
	 
	 public static WebElement  Close_Add_Absences (WebDriver driver){
		 
		  element = driver.findElement(By.className("btnWorkCardCloseCard"));
		    
		 
		   return element;  }
	 
	 
	 
	 
	 public static WebElement  End_Date_Absences (WebDriver driver){
		 
		  element = driver.findElement(By.id("clnEndDateHeadrut"));
		    
		 
		   return element;  }
	 
	 
	 
	
	
	
	

}
