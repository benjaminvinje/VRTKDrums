// Copyright 2017 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class caracaDeviceInterface : deviceInterface {
  caracaSignalGenerator signal;
  caracaUI _caracaUI;
  omniJack jackOut;
  double _sampleDuration;

  [DllImport("SoundStageNative")]
  public static extern void CaracaProcessAudioBuffer(float[] buffer, float[] controlBuffer, int length, int channels, ref double _phase, double _sampleDuration);

  public override void Awake() {
    base.Awake();
    _sampleDuration = 1.0 / AudioSettings.outputSampleRate;
    signal = GetComponent<caracaSignalGenerator>();
    _caracaUI = GetComponentInChildren<caracaUI>();
    jackOut = GetComponentInChildren<omniJack>();
  }

  void Update() {
    signal.curShake = _caracaUI.shakeVal;
  }

  void OnDestroy() {
    if (_caracaUI.transform.parent != transform) Destroy(_caracaUI.gameObject);
  }

  double _phaseB = 0;
  private void OnAudioFilterRead(float[] buffer, int channels) {
    if (jackOut.near != null || signal.curShake < .01f) return;

    double dspTime = AudioSettings.dspTime;
    float[] b = new float[buffer.Length];
    signal.processBuffer(b, dspTime, channels);

    CaracaProcessAudioBuffer(buffer, b, buffer.Length, channels, ref _phaseB, _sampleDuration);
  }

  public override InstrumentData GetData() {
    CaracaData data = new CaracaData();
    data.deviceType = menuItem.deviceType.Caracas;
    GetTransformData(data);
    data.jackOutID = jackOut.transform.GetInstanceID();
    return data;
  }

  public override void Load(InstrumentData d) {
    CaracaData data = d as CaracaData;
    base.Load(data);
    jackOut.ID = data.jackOutID;
  }
}

public class CaracaData : InstrumentData {
  public int jackOutID;
}