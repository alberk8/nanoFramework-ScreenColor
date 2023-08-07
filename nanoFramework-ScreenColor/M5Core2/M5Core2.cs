using Iot.Device.Axp192;
using nanoFramework.Hardware.Esp32;
using System.Device.Gpio;
using System.Device.I2c;
using System.Threading;
using UnitsNet;

namespace nanoFramework_ScreenColor.M5Core2
{
    public static class MCore2
    {

        private static Axp192 _power;
        public static void PowerOn()
        {
            Configuration.SetPinFunction(22, DeviceFunction.I2C1_CLOCK);
            Configuration.SetPinFunction(21, DeviceFunction.I2C1_DATA);

            // Create the energy management device
            I2cDevice i2c = new(new I2cConnectionSettings(1, Axp192.I2cDefaultAddress));
            _power = new(i2c);

            // Configuration common for M5Core2 and M5Tough

            // VBUS-IPSOUT Pass-Through Management
            _power.SetVbusSettings(false, false, VholdVoltage.V4_0, true, VbusCurrentLimit.MilliAmper500);
            // Set Power off voltage 3.0v
            _power.VoffVoltage = VoffVoltage.V3_0;
            // Enable bat detection
            _power.SetShutdownBatteryDetectionControl(false, true, ShutdownBatteryPinFunction.HighResistance, false, ShutdownBatteryTiming.S2);
            // Bat charge voltage to 4.2, Current 100MA
            _power.SetChargingFunctions(true, ChargingVoltage.V4_2, ChargingCurrent.Current100mA, ChargingStopThreshold.Percent10);
            // Enable RTC BAT charge 
            _power.SetBackupBatteryChargingControl(true, BackupBatteryCharingVoltage.V3_0, BackupBatteryChargingCurrent.MicroAmperes200);

            // Set ADC all on
            _power.AdcPinEnabled = AdcPinEnabled.All;
            // Set ADC sample rate to 25Hz
            _power.AdcFrequency = AdcFrequency.Frequency25Hz;
            _power.AdcPinCurrent = AdcPinCurrent.MicroAmperes80;
            _power.BatteryTemperatureMonitoring = true;
            _power.AdcPinCurrentSetting = AdcPinCurrentSetting.AlwaysOn;

            // GPIO0 is LDO
            _power.Gpio0Behavior = Gpio0Behavior.LowNoiseLDO;
            // GPIO0 LDO output 2.8V
            _power.PinOutputVoltage = PinOutputVoltage.V2_8;
            // Sets DCDC1 3350mV (ESP32 VDD)
            _power.DcDc1Voltage = ElectricPotential.FromVolts(3.35);

            // LCD and peripherals power supply
            _power.LDO2OutputVoltage = ElectricPotential.FromVolts(3.3);
            _power.EnableLDO2(true);

            // GPIO2 enables the speaker 
            _power.Gpio2Behavior = Gpio12Behavior.MnosLeakOpenOutput;

            // GPIO4 LCD reset (and also Touch controller reset on M5Core2)
            _power.Gpio4Behavior = Gpio4Behavior.MnosLeakOpenOutput;

            // Set temperature protection
            _power.SetBatteryHighTemperatureThreshold(ElectricPotential.FromVolts(3.2256));

            // This part of the code will handle the button behavior
            _power.EnableButtonPressed(ButtonPressed.LongPressed | ButtonPressed.ShortPressed);
            // 128ms power on, 4s power off
            _power.SetButtonBehavior(LongPressTiming.S1, ShortPressTiming.Ms128, true, SignalDelayAfterPowerUp.Ms32, ShutdownTiming.S4);

            // enable EXTEN switch control to enable 5V boost
            _power.EXTENEnable = true;

            _power.LdoDcPinsEnabled = LdoDcPinsEnabled.DcDc1 | LdoDcPinsEnabled.DcDc3 | LdoDcPinsEnabled.Ldo2 | LdoDcPinsEnabled.Ldo3;


            _power.DcDc3Voltage = ElectricPotential.FromVolts(2.8);

            _power.ChargingCurrent = ChargingCurrent.Current360mA;

            //Vibrate turn off
            _power.LDO3OutputVoltage = ElectricPotential.FromVolts(2.0);
            _power.EnableLDO3(false);

            _power.Gpio4Value = PinValue.Low;
            Thread.Sleep(100);
            _power.Gpio4Value = PinValue.High;
            Thread.Sleep(100);

            // Screen Turn On
            _power.EnableDCDC3(true);
        }
    }
}
