package Listener;

import java.io.File;
import java.io.IOException;

import org.apache.commons.io.FileUtils;
import org.junit.Before;
import org.openqa.selenium.OutputType;
import org.openqa.selenium.TakesScreenshot;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.ie.InternetExplorerDriver;
import org.openqa.selenium.remote.Augmenter;
import org.testng.ITestContext;
import org.testng.ITestListener;
import org.testng.ITestResult;
import org.openqa.selenium.WebDriver;
//import com.pack.sample.TestBase;




public class TestListener implements ITestListener {
	public static  WebDriver driver;
	//WebDriver driver=null;
	
	String filePath = "C:\\SCREENSHOTS";
    @Override
    public void onTestFailure(ITestResult result) {
    	System.out.println("***** Error "+result.getName()+" test has failed *****");
    	String methodName=result.getName().toString().trim();
    	takeScreenShot(methodName);
    }
    
    
    public void takeScreenShot(String methodName) {
    
    	//get the driver
    	//driver=TestBase.getDriver();
    	//driver=Base.Initialize_browser();
    	//Base.Initialize_Webpage(driver);
    	// driver = new InternetExplorerDriver();
    	//File file = new File("C:/Selenium/iexploredriver.exe");
    	//System.setProperty("webdriver.ie.driver", file.getAbsolutePath());
    	//WebDriver driver = new InternetExplorerDriver();
    	
    	File scrFile = ((TakesScreenshot)driver).getScreenshotAs(OutputType.FILE);
    	
         //The below method will save the screen shot in d drive with test method name 
            try {
				FileUtils.copyFile(scrFile, new File(filePath+methodName+".png"));
				System.out.println("***Placed screen shot in "+filePath+" ***");
			} catch (IOException e) {
				e.printStackTrace();
			}
    }
	public void onFinish(ITestContext context) {}
  
    public void onTestStart(ITestResult result) {   }
  
    public void onTestSuccess(ITestResult result) {   }

    public void onTestSkipped(ITestResult result) {   }

    public void onTestFailedButWithinSuccessPercentage(ITestResult result) {   }

    public void onStart(ITestContext context) {   }
}  



