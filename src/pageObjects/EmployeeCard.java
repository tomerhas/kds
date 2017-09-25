package pageObjects;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

public class EmployeeCard {
	
	private static WebElement element ;
	
	
	   
	
	public static WebElement Txt_Id(WebDriver driver) {
		
		element=driver.findElement(By.id("ctl00_KdsContent_txtId"));
		
		return element;
		
	}
	
	
	
public static WebElement List_Month(WebDriver driver) {
	   
		element=driver.findElement(By.id("ctl00_KdsContent_ddlMonth"));
		
		return element;
		
	}
	
	
	
	
	
public static WebElement Btn_Execute(WebDriver driver) {
		
		element=driver.findElement(By.id("ctl00_KdsContent_btnExecute"));
		
		return element;
		
	}



public static WebElement Rdo_Id(WebDriver driver) {
	
	element=driver.findElement(By.id("ctl00_KdsContent_rdoId"));
	
	return element;
	
}



public static WebElement Rdo_Name(WebDriver driver) {
	
	element=driver.findElement(By.id("ctl00_KdsContent_rdoName"));
	
	return element;
	
}


public static WebElement Rdo_Month(WebDriver driver) {
	
	element=driver.findElement(By.id("ctl00_KdsContent_rdoMonth"));
	
	return element;
	
}




public static WebElement Txt_name(WebDriver driver) {
	
	element=driver.findElement(By.id("ctl00_KdsContent_txtName"));
	
	return element;
	
}

	
	
public static  WebElement  Grd_Employee(WebDriver driver) {
	
	element=driver.findElement(By.id("ctl00_KdsContent_grdEmployee"));
	
	return element;
	
}
	
	

	


public static  WebElement  Link_Date(WebDriver driver) {

element=driver.findElement(By.linkText("10/01/2017 �"));

return element;

}

	

public static  WebElement  Link_Date_Menahel_Bameshek(WebDriver driver) {

element=driver.findElement(By.linkText("24/11/2016 �"));

return element;

}


	
	
	
}
