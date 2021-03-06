// As of Rust 1.34.0, these dependencies need to be declared in this order using
// `extern crate` in your `main.rs` file. See
// https://github.com/emk/rust-musl-builder/issues/69.
extern crate openssl;

#[macro_use]
extern crate diesel;

#[macro_use]
extern crate diesel_migrations;

embed_migrations!("./migrations");

extern crate dotenv;

use actix_web::{middleware::Logger, web, App, HttpServer};
use diesel::{pg::PgConnection, r2d2::ConnectionManager, r2d2::Pool};
use dotenv::dotenv;
use embedded_migrations::run;
use std::{env::var, io::Result};

mod database;
mod handlers;
mod models;
mod schema;

#[actix_rt::main]
async fn main() -> Result<()> {
    dotenv().ok();

    env_logger::init();

    let address = var("IP_ADDRESS").expect("IP_ADDRESS not found");
    let database_url = var("DATABASE_URL").expect("DATABASE_URL not found");
    let manager = ConnectionManager::<PgConnection>::new(database_url);
    let pool = Pool::builder()
        .build(manager)
        .expect("Failed to create pool");

    run(&*pool.clone().get().expect("No db connection")).expect("Could not run migrations");

    println!("Server is running {}", address);

    HttpServer::new(move || {
        App::new()
            .wrap(Logger::default())
            .data(pool.clone())
            .route("/characters", web::post().to(handlers::create))
            .route("/characters/{id}", web::delete().to(handlers::delete_by_id))
            .route("/characters/{id}", web::get().to(handlers::get_all))
    })
    .bind(address)?
    .run()
    .await
}
