﻿using StackExchange.Redis;

namespace MultiShop.Basket.Settings
{
    public class RedisService
{
    public string _host { get; set; }
    public int _port { get; set; }

    private ConnectionMultiplexer _connectionMultiplexer;

    public RedisService(string host, int port)
    {
        _port = port;
        _host = host;

        // Bağlantıyı burada yapıyoruz
        Connect();
    }

    public void Connect()
    {
        _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");
    }

    public IDatabase GetDb(int db = 1)
    {
        // _connectionMultiplexer null ise Exception fırlat
        if (_connectionMultiplexer == null)
            throw new InvalidOperationException("Redis connection is not established.");

        return _connectionMultiplexer.GetDatabase(db); // Belirtilen veritabanını al
    }
}
    
}
