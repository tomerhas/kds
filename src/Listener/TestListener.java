package Listener;

import java.io.File;
import java.io.IOException;

import org.apache.commons.io.FileUtils;
import org.openqa.selenium.OutputType;
import org.openqa.selenium.TakesScreenshot;
import org.openqa.selenium.WebDriver;
import org.testng.ITestResult;
import org.testng.TestListenerAdapter;
import utils.Base;


 


public class TestListener extends TestListenerAdapter{ 
           
     @Override
     public void onTestFailure(ITestResult result){ 
            CaptureScreenShot(result);
            System.out.println(result.getName()+" Test Failed \n");
     }
           
     @Override
     public void onTestSuccess(ITestResult result){
           System.out.println(result.getName()+" Test Passed \n");
     }
           
     @Override
     public void onTestSkipped(ITestResult result){
           System.out.println(result.getName()+" Test Skipped \n");
     }
            
     public void CaptureScreenShot(ITestResult result){
           Object obj  = result.getInstance();
           WebDriver driver =((Base) obj).getDriver();
                        
           File scrFile = ((TakesScreenshot) driver).getScreenshotAs(OutputType.FILE);
                                         
           try {
        	      FileUtils.copyFile(scrFile, new File("C:\\selenium\\workspace\\KDS_SELENIUM\\SCREENSHOTS\\"+ result.getName()+".png"));
                  //FileUtils.copyFile(scrFile, new File("C:\\SCREENSHOTS\\failure.png"));
           }
           catch (IOException e) {
                e.printStackTrace();
           }
      } 
}









