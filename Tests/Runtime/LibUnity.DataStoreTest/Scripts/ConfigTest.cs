﻿using System;
using System.Collections.Generic;
using LibUnity.DataStore;
using LibUnity.UnitTest;
using UnityEngine;

namespace LibUnity.DataStoreTest {
  /**
   * \class ConfigTest
   *
   * \brief query 모듈 테스트 케이스
   *
   * \author Lee, Hyeon-gi
   */
  class ConfigTest : TestCase {
    [TestMethod]
    public void TestGet() {
      con = JsonStore.Create(new ResourcesIO("LibUnity.DataStoreTest/environment"));
      Assert(con.Get<int>("test_config.var1") == 0, "get test_config.var1 is 0");
      Assert(con.Get<long>("test_config.var2") == 533157870896947200, "get test_config.var1 is 0");
      //Assert(con.Get<int>("test_config.var1") == 0, "get test_config.var1 is 0");
    }

    [TestMethod]
    public void testSet() {
      con = JsonStore.Create(new ResourcesIO("LibUnity.DataStoreTest/environment"));
      con.Set<long>("test_config.var1", 10000);
      Assert(con.Get<long>("test_config.var1") == 10000, "get test_config.var1 is 10000");

      con.Set<long>("test_config.var2", 10);
      Assert(con.Get<long>("test_config.var2") == 10, "get test_config.var1 is 10000");
    }

    [TestMethod]
    public void TestGetFailed() {
      con = JsonStore.Create(new ResourcesIO("LibUnity.DataStoreTest/environment"));
      Assert(!IsGetSuccess<string>(con, "test_config2.var"), "exception when wrong config name");
      Assert(IsGetSuccess<int>(con, "test_config.var1"), "success when valid type");
      Assert(!IsGetSuccess<string>(con, "test_config.var1"), "exception when wrong type");
      Assert(IsGetSuccess<double>(con, "test_config.var_float"), "success when cast enable type");
      Assert(!IsGetSuccess<float>(con, "test_config.var_float"), "exception when wrong type");
      Assert(!IsGetSuccess<string>(con, "test_config.var3333"), "exception when not exist path");
    }

    override protected void SetUp() {
    }

    override protected void TearDown() {
    }

    private Query con = null;

    private bool IsGetSuccess<T>(Query query, string path) {
#pragma warning disable 0219, 0168
      bool rise_exception = false;
      try {
        T var = query.Get<T>(path);
      }
      catch (System.Exception e) {
        rise_exception = true;
      }
      return !rise_exception;
#pragma warning restore 0219, 0168
    }
  }
}
