package com.example.domain.dto

import kotlinx.serialization.Serializable

@Serializable
data class UserData(
    val id: Int = 0,
    val name: String = "",
    val username: String = "",
    var email: String = ""
)