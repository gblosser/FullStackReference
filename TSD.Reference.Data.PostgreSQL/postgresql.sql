create TABLE automobile (
    id integer not null default nextval('automobile_id_seq'::regclass),
    vin text,
    vehiclenumber text,
    name text,
    class text,
    style text,
    color text,
    manufacturer text,
    model text,
    code text,
    locationid integer,
    PRIMARY KEY (id),
    FOREIGN KEY (locationid) REFERENCES location (id)
);
CREATE UNIQUE INDEX automobile_primary_key ON automobile (id);
CREATE UNIQUE INDEX Unique_VIN ON automobile (vin);
create TABLE customer (
    id integer not null default nextval('customer_id_seq'::regclass),
    name text,
    allowsadditionaldrivers boolean not null default true,
    allowsadditions boolean not null default false,
    hasmaxrentaldays boolean not null default true,
    maxrentaldays integer not null default 28,
    PRIMARY KEY (id)
);
CREATE UNIQUE INDEX customer_primary_key ON customer (id);
create TABLE driver (
    id integer not null default nextval('driver_id_seq'::regclass),
    firstname text,
    lastname text,
    address text,
    city text,
    state text,
    postalcode text,
    country text,
    licensenumber text,
    licensestate text,
    customerid integer,
    PRIMARY KEY (id),
    FOREIGN KEY (customerid) REFERENCES customer (id)
);
CREATE UNIQUE INDEX driver_primary_key ON driver (id);
create TABLE location (
    id integer not null default nextval('location_id_seq'::regclass),
    customerid integer,
    name text,
    address text,
    city text,
    state text,
    postalcode text,
    country text,
    latitude real,
    longitude real,
    PRIMARY KEY (id),
    FOREIGN KEY (customerid) REFERENCES customer (id)
);
CREATE UNIQUE INDEX location_primary_key ON location (id);
create TABLE rentalagreement (
    id integer not null default nextval('rentalagreement_id_seq'::regclass),
    customerid integer,
    locationid integer,
    renterid integer,
    additionaldrivers text,
    outdate time,
    indate timestamp,
    automobileid integer,
    additions text,
    status text,
    employeeid integer,
    PRIMARY KEY (id),
    FOREIGN KEY (customerid) REFERENCES customer (id),
    FOREIGN KEY (locationid) REFERENCES location (id),
    FOREIGN KEY (renterid) REFERENCES renter (id),
    FOREIGN KEY (automobileid) REFERENCES automobile (id),
    FOREIGN KEY (employeeid) REFERENCES "user" (id)
);
CREATE UNIQUE INDEX rentalagreementprimarykey ON rentalagreement (id);
create TABLE renter (
    id integer not null default nextval('renter_id_seq'::regclass),
    firstname text,
    lastname text,
    address text,
    city text,
    state text,
    postalcode text,
    country text,
    licensenumber text,
    licensestate text,
    customerid integer,
    PRIMARY KEY (id),
    FOREIGN KEY (customerid) REFERENCES customer (id)
);
CREATE UNIQUE INDEX renterprimarykey ON renter (id);
create TABLE "user" (
    id integer not null,
    firstname text,
    lastname text,
    email text,
    customerid integer,
    isemployee boolean,
    PRIMARY KEY (id),
    FOREIGN KEY (customerid) REFERENCES customer (id)
);
CREATE UNIQUE INDEX userid ON "user" (id);
create TABLE pg_aggregate (
    aggfnoid regproc not null,
    aggkind "char" not null,
    aggnumdirectargs smallint not null,
    aggtransfn regproc not null,
    aggfinalfn regproc not null,
    aggmtransfn regproc not null,
    aggminvtransfn regproc not null,
    aggmfinalfn regproc not null,
    aggfinalextra boolean not null,
    aggmfinalextra boolean not null,
    aggsortop oid not null,
    aggtranstype oid not null,
    aggtransspace integer not null,
    aggmtranstype oid not null,
    aggmtransspace integer not null,
    agginitval text,
    aggminitval text
);
CREATE UNIQUE INDEX pg_aggregate_fnoid_index ON pg_aggregate (aggfnoid);
create TABLE pg_am (
    amname name not null,
    amstrategies smallint not null,
    amsupport smallint not null,
    amcanorder boolean not null,
    amcanorderbyop boolean not null,
    amcanbackward boolean not null,
    amcanunique boolean not null,
    amcanmulticol boolean not null,
    amoptionalkey boolean not null,
    amsearcharray boolean not null,
    amsearchnulls boolean not null,
    amstorage boolean not null,
    amclusterable boolean not null,
    ampredlocks boolean not null,
    amkeytype oid not null,
    aminsert regproc not null,
    ambeginscan regproc not null,
    amgettuple regproc not null,
    amgetbitmap regproc not null,
    amrescan regproc not null,
    amendscan regproc not null,
    ammarkpos regproc not null,
    amrestrpos regproc not null,
    ambuild regproc not null,
    ambuildempty regproc not null,
    ambulkdelete regproc not null,
    amvacuumcleanup regproc not null,
    amcanreturn regproc not null,
    amcostestimate regproc not null,
    amoptions regproc not null
);
CREATE UNIQUE INDEX pg_am_name_index ON pg_am (amname);
CREATE UNIQUE INDEX pg_am_oid_index ON pg_am (oid);
create TABLE pg_amop (
    amopfamily oid not null,
    amoplefttype oid not null,
    amoprighttype oid not null,
    amopstrategy smallint not null,
    amoppurpose "char" not null,
    amopopr oid not null,
    amopmethod oid not null,
    amopsortfamily oid not null
);
CREATE UNIQUE INDEX pg_amop_fam_strat_index ON pg_amop (amopfamily, amoplefttype, amoprighttype, amopstrategy);
CREATE UNIQUE INDEX pg_amop_opr_fam_index ON pg_amop (amopopr, amoppurpose, amopfamily);
CREATE UNIQUE INDEX pg_amop_oid_index ON pg_amop (oid);
create TABLE pg_amproc (
    amprocfamily oid not null,
    amproclefttype oid not null,
    amprocrighttype oid not null,
    amprocnum smallint not null,
    amproc regproc not null
);
CREATE UNIQUE INDEX pg_amproc_fam_proc_index ON pg_amproc (amprocfamily, amproclefttype, amprocrighttype, amprocnum);
CREATE UNIQUE INDEX pg_amproc_oid_index ON pg_amproc (oid);
create TABLE pg_attrdef (
    adrelid oid not null,
    adnum smallint not null,
    adbin pg_node_tree,
    adsrc text
);
CREATE UNIQUE INDEX pg_attrdef_adrelid_adnum_index ON pg_attrdef (adrelid, adnum);
CREATE UNIQUE INDEX pg_attrdef_oid_index ON pg_attrdef (oid);
create TABLE pg_attribute (
    attrelid oid not null,
    attname name not null,
    atttypid oid not null,
    attstattarget integer not null,
    attlen smallint not null,
    attnum smallint not null,
    attndims integer not null,
    attcacheoff integer not null,
    atttypmod integer not null,
    attbyval boolean not null,
    attstorage "char" not null,
    attalign "char" not null,
    attnotnull boolean not null,
    atthasdef boolean not null,
    attisdropped boolean not null,
    attislocal boolean not null,
    attinhcount integer not null,
    attcollation oid not null,
    attacl aclitem[],
    attoptions text[],
    attfdwoptions text[]
);
CREATE UNIQUE INDEX pg_attribute_relid_attnam_index ON pg_attribute (attrelid, attname);
CREATE UNIQUE INDEX pg_attribute_relid_attnum_index ON pg_attribute (attrelid, attnum);
create TABLE pg_auth_members (
    roleid oid not null,
    member oid not null,
    grantor oid not null,
    admin_option boolean not null
);
CREATE UNIQUE INDEX pg_auth_members_role_member_index ON pg_auth_members (roleid, member);
CREATE UNIQUE INDEX pg_auth_members_member_role_index ON pg_auth_members (member, roleid);
create TABLE pg_authid (
    rolname name not null,
    rolsuper boolean not null,
    rolinherit boolean not null,
    rolcreaterole boolean not null,
    rolcreatedb boolean not null,
    rolcatupdate boolean not null,
    rolcanlogin boolean not null,
    rolreplication boolean not null,
    rolconnlimit integer not null,
    rolpassword text,
    rolvaliduntil timestamp with time zone
);
CREATE UNIQUE INDEX pg_authid_rolname_index ON pg_authid (rolname);
CREATE UNIQUE INDEX pg_authid_oid_index ON pg_authid (oid);
create TABLE pg_cast (
    castsource oid not null,
    casttarget oid not null,
    castfunc oid not null,
    castcontext "char" not null,
    castmethod "char" not null
);
CREATE UNIQUE INDEX pg_cast_source_target_index ON pg_cast (castsource, casttarget);
CREATE UNIQUE INDEX pg_cast_oid_index ON pg_cast (oid);
create TABLE pg_class (
    relname name not null,
    relnamespace oid not null,
    reltype oid not null,
    reloftype oid not null,
    relowner oid not null,
    relam oid not null,
    relfilenode oid not null,
    reltablespace oid not null,
    relpages integer not null,
    reltuples real not null,
    relallvisible integer not null,
    reltoastrelid oid not null,
    relhasindex boolean not null,
    relisshared boolean not null,
    relpersistence "char" not null,
    relkind "char" not null,
    relnatts smallint not null,
    relchecks smallint not null,
    relhasoids boolean not null,
    relhaspkey boolean not null,
    relhasrules boolean not null,
    relhastriggers boolean not null,
    relhassubclass boolean not null,
    relispopulated boolean not null,
    relreplident "char" not null,
    relfrozenxid xid not null,
    relminmxid xid not null,
    relacl aclitem[],
    reloptions text[]
);
CREATE UNIQUE INDEX pg_class_relname_nsp_index ON pg_class (relname, relnamespace);
CREATE UNIQUE INDEX pg_class_oid_index ON pg_class (oid);
CREATE INDEX pg_class_tblspc_relfilenode_index ON pg_class (reltablespace, relfilenode);
create TABLE pg_collation (
    collname name not null,
    collnamespace oid not null,
    collowner oid not null,
    collencoding integer not null,
    collcollate name not null,
    collctype name not null
);
CREATE UNIQUE INDEX pg_collation_name_enc_nsp_index ON pg_collation (collname, collencoding, collnamespace);
CREATE UNIQUE INDEX pg_collation_oid_index ON pg_collation (oid);
create TABLE pg_constraint (
    conname name not null,
    connamespace oid not null,
    contype "char" not null,
    condeferrable boolean not null,
    condeferred boolean not null,
    convalidated boolean not null,
    conrelid oid not null,
    contypid oid not null,
    conindid oid not null,
    confrelid oid not null,
    confupdtype "char" not null,
    confdeltype "char" not null,
    confmatchtype "char" not null,
    conislocal boolean not null,
    coninhcount integer not null,
    connoinherit boolean not null,
    conkey smallint[],
    confkey smallint[],
    conpfeqop oid[],
    conppeqop oid[],
    conffeqop oid[],
    conexclop oid[],
    conbin pg_node_tree,
    consrc text
);
CREATE UNIQUE INDEX pg_constraint_oid_index ON pg_constraint (oid);
CREATE INDEX pg_constraint_conname_nsp_index ON pg_constraint (conname, connamespace);
CREATE INDEX pg_constraint_conrelid_index ON pg_constraint (conrelid);
CREATE INDEX pg_constraint_contypid_index ON pg_constraint (contypid);
create TABLE pg_conversion (
    conname name not null,
    connamespace oid not null,
    conowner oid not null,
    conforencoding integer not null,
    contoencoding integer not null,
    conproc regproc not null,
    condefault boolean not null
);
CREATE UNIQUE INDEX pg_conversion_name_nsp_index ON pg_conversion (conname, connamespace);
CREATE UNIQUE INDEX pg_conversion_default_index ON pg_conversion (connamespace, conforencoding, contoencoding, oid);
CREATE UNIQUE INDEX pg_conversion_oid_index ON pg_conversion (oid);
create TABLE pg_database (
    datname name not null,
    datdba oid not null,
    encoding integer not null,
    datcollate name not null,
    datctype name not null,
    datistemplate boolean not null,
    datallowconn boolean not null,
    datconnlimit integer not null,
    datlastsysoid oid not null,
    datfrozenxid xid not null,
    datminmxid xid not null,
    dattablespace oid not null,
    datacl aclitem[]
);
CREATE UNIQUE INDEX pg_database_datname_index ON pg_database (datname);
CREATE UNIQUE INDEX pg_database_oid_index ON pg_database (oid);
create TABLE pg_db_role_setting (
    setdatabase oid not null,
    setrole oid not null,
    setconfig text[]
);
CREATE UNIQUE INDEX pg_db_role_setting_databaseid_rol_index ON pg_db_role_setting (setdatabase, setrole);
create TABLE pg_default_acl (
    defaclrole oid not null,
    defaclnamespace oid not null,
    defaclobjtype "char" not null,
    defaclacl aclitem[]
);
CREATE UNIQUE INDEX pg_default_acl_role_nsp_obj_index ON pg_default_acl (defaclrole, defaclnamespace, defaclobjtype);
CREATE UNIQUE INDEX pg_default_acl_oid_index ON pg_default_acl (oid);
create TABLE pg_depend (
    classid oid not null,
    objid oid not null,
    objsubid integer not null,
    refclassid oid not null,
    refobjid oid not null,
    refobjsubid integer not null,
    deptype "char" not null
);
CREATE INDEX pg_depend_depender_index ON pg_depend (classid, objid, objsubid);
CREATE INDEX pg_depend_reference_index ON pg_depend (refclassid, refobjid, refobjsubid);
create TABLE pg_description (
    objoid oid not null,
    classoid oid not null,
    objsubid integer not null,
    description text
);
CREATE UNIQUE INDEX pg_description_o_c_o_index ON pg_description (objoid, classoid, objsubid);
create TABLE pg_enum (
    enumtypid oid not null,
    enumsortorder real not null,
    enumlabel name not null
);
CREATE UNIQUE INDEX pg_enum_typid_sortorder_index ON pg_enum (enumtypid, enumsortorder);
CREATE UNIQUE INDEX pg_enum_typid_label_index ON pg_enum (enumtypid, enumlabel);
CREATE UNIQUE INDEX pg_enum_oid_index ON pg_enum (oid);
create TABLE pg_event_trigger (
    evtname name not null,
    evtevent name not null,
    evtowner oid not null,
    evtfoid oid not null,
    evtenabled "char" not null,
    evttags text[]
);
CREATE UNIQUE INDEX pg_event_trigger_evtname_index ON pg_event_trigger (evtname);
CREATE UNIQUE INDEX pg_event_trigger_oid_index ON pg_event_trigger (oid);
create TABLE pg_extension (
    extname name not null,
    extowner oid not null,
    extnamespace oid not null,
    extrelocatable boolean not null,
    extversion text,
    extconfig oid[],
    extcondition text[]
);
CREATE UNIQUE INDEX pg_extension_name_index ON pg_extension (extname);
CREATE UNIQUE INDEX pg_extension_oid_index ON pg_extension (oid);
create TABLE pg_foreign_data_wrapper (
    fdwname name not null,
    fdwowner oid not null,
    fdwhandler oid not null,
    fdwvalidator oid not null,
    fdwacl aclitem[],
    fdwoptions text[]
);
CREATE UNIQUE INDEX pg_foreign_data_wrapper_name_index ON pg_foreign_data_wrapper (fdwname);
CREATE UNIQUE INDEX pg_foreign_data_wrapper_oid_index ON pg_foreign_data_wrapper (oid);
create TABLE pg_foreign_server (
    srvname name not null,
    srvowner oid not null,
    srvfdw oid not null,
    srvtype text,
    srvversion text,
    srvacl aclitem[],
    srvoptions text[]
);
CREATE UNIQUE INDEX pg_foreign_server_name_index ON pg_foreign_server (srvname);
CREATE UNIQUE INDEX pg_foreign_server_oid_index ON pg_foreign_server (oid);
create TABLE pg_foreign_table (
    ftrelid oid not null,
    ftserver oid not null,
    ftoptions text[]
);
CREATE UNIQUE INDEX pg_foreign_table_relid_index ON pg_foreign_table (ftrelid);
create TABLE pg_index (
    indexrelid oid not null,
    indrelid oid not null,
    indnatts smallint not null,
    indisunique boolean not null,
    indisprimary boolean not null,
    indisexclusion boolean not null,
    indimmediate boolean not null,
    indisclustered boolean not null,
    indisvalid boolean not null,
    indcheckxmin boolean not null,
    indisready boolean not null,
    indislive boolean not null,
    indisreplident boolean not null,
    indkey int2vector not null,
    indcollation oidvector not null,
    indclass oidvector not null,
    indoption int2vector not null,
    indexprs pg_node_tree,
    indpred pg_node_tree
);
CREATE UNIQUE INDEX pg_index_indexrelid_index ON pg_index (indexrelid);
CREATE INDEX pg_index_indrelid_index ON pg_index (indrelid);
create TABLE pg_inherits (
    inhrelid oid not null,
    inhparent oid not null,
    inhseqno integer not null
);
CREATE UNIQUE INDEX pg_inherits_relid_seqno_index ON pg_inherits (inhrelid, inhseqno);
CREATE INDEX pg_inherits_parent_index ON pg_inherits (inhparent);
create TABLE pg_language (
    lanname name not null,
    lanowner oid not null,
    lanispl boolean not null,
    lanpltrusted boolean not null,
    lanplcallfoid oid not null,
    laninline oid not null,
    lanvalidator oid not null,
    lanacl aclitem[]
);
CREATE UNIQUE INDEX pg_language_name_index ON pg_language (lanname);
CREATE UNIQUE INDEX pg_language_oid_index ON pg_language (oid);
create TABLE pg_largeobject (
    loid oid not null,
    pageno integer not null,
    data bytea
);
CREATE UNIQUE INDEX pg_largeobject_loid_pn_index ON pg_largeobject (loid, pageno);
create TABLE pg_largeobject_metadata (
    lomowner oid not null,
    lomacl aclitem[]
);
CREATE UNIQUE INDEX pg_largeobject_metadata_oid_index ON pg_largeobject_metadata (oid);
create TABLE pg_namespace (
    nspname name not null,
    nspowner oid not null,
    nspacl aclitem[]
);
CREATE UNIQUE INDEX pg_namespace_nspname_index ON pg_namespace (nspname);
CREATE UNIQUE INDEX pg_namespace_oid_index ON pg_namespace (oid);
create TABLE pg_opclass (
    opcmethod oid not null,
    opcname name not null,
    opcnamespace oid not null,
    opcowner oid not null,
    opcfamily oid not null,
    opcintype oid not null,
    opcdefault boolean not null,
    opckeytype oid not null
);
CREATE UNIQUE INDEX pg_opclass_am_name_nsp_index ON pg_opclass (opcmethod, opcname, opcnamespace);
CREATE UNIQUE INDEX pg_opclass_oid_index ON pg_opclass (oid);
create TABLE pg_operator (
    oprname name not null,
    oprnamespace oid not null,
    oprowner oid not null,
    oprkind "char" not null,
    oprcanmerge boolean not null,
    oprcanhash boolean not null,
    oprleft oid not null,
    oprright oid not null,
    oprresult oid not null,
    oprcom oid not null,
    oprnegate oid not null,
    oprcode regproc not null,
    oprrest regproc not null,
    oprjoin regproc not null
);
CREATE UNIQUE INDEX pg_operator_oprname_l_r_n_index ON pg_operator (oprname, oprleft, oprright, oprnamespace);
CREATE UNIQUE INDEX pg_operator_oid_index ON pg_operator (oid);
create TABLE pg_opfamily (
    opfmethod oid not null,
    opfname name not null,
    opfnamespace oid not null,
    opfowner oid not null
);
CREATE UNIQUE INDEX pg_opfamily_am_name_nsp_index ON pg_opfamily (opfmethod, opfname, opfnamespace);
CREATE UNIQUE INDEX pg_opfamily_oid_index ON pg_opfamily (oid);
create TABLE pg_pltemplate (
    tmplname name not null,
    tmpltrusted boolean not null,
    tmpldbacreate boolean not null,
    tmplhandler text,
    tmplinline text,
    tmplvalidator text,
    tmpllibrary text,
    tmplacl aclitem[]
);
CREATE UNIQUE INDEX pg_pltemplate_name_index ON pg_pltemplate (tmplname);
create TABLE pg_proc (
    proname name not null,
    pronamespace oid not null,
    proowner oid not null,
    prolang oid not null,
    procost real not null,
    prorows real not null,
    provariadic oid not null,
    protransform regproc not null,
    proisagg boolean not null,
    proiswindow boolean not null,
    prosecdef boolean not null,
    proleakproof boolean not null,
    proisstrict boolean not null,
    proretset boolean not null,
    provolatile "char" not null,
    pronargs smallint not null,
    pronargdefaults smallint not null,
    prorettype oid not null,
    proargtypes oidvector not null,
    proallargtypes oid[],
    proargmodes "char"[],
    proargnames text[],
    proargdefaults pg_node_tree,
    prosrc text,
    probin text,
    proconfig text[],
    proacl aclitem[]
);
CREATE UNIQUE INDEX pg_proc_proname_args_nsp_index ON pg_proc (proname, proargtypes, pronamespace);
CREATE UNIQUE INDEX pg_proc_oid_index ON pg_proc (oid);
create TABLE pg_range (
    rngtypid oid not null,
    rngsubtype oid not null,
    rngcollation oid not null,
    rngsubopc oid not null,
    rngcanonical regproc not null,
    rngsubdiff regproc not null
);
CREATE UNIQUE INDEX pg_range_rngtypid_index ON pg_range (rngtypid);
create TABLE pg_rewrite (
    rulename name not null,
    ev_class oid not null,
    ev_type "char" not null,
    ev_enabled "char" not null,
    is_instead boolean not null,
    ev_qual pg_node_tree,
    ev_action pg_node_tree
);
CREATE UNIQUE INDEX pg_rewrite_rel_rulename_index ON pg_rewrite (ev_class, rulename);
CREATE UNIQUE INDEX pg_rewrite_oid_index ON pg_rewrite (oid);
create TABLE pg_seclabel (
    objoid oid not null,
    classoid oid not null,
    objsubid integer not null,
    provider text,
    label text
);
CREATE UNIQUE INDEX pg_seclabel_object_index ON pg_seclabel (objoid, classoid, objsubid, provider);
create TABLE pg_shdepend (
    dbid oid not null,
    classid oid not null,
    objid oid not null,
    objsubid integer not null,
    refclassid oid not null,
    refobjid oid not null,
    deptype "char" not null
);
CREATE INDEX pg_shdepend_depender_index ON pg_shdepend (dbid, classid, objid, objsubid);
CREATE INDEX pg_shdepend_reference_index ON pg_shdepend (refclassid, refobjid);
create TABLE pg_shdescription (
    objoid oid not null,
    classoid oid not null,
    description text
);
CREATE UNIQUE INDEX pg_shdescription_o_c_index ON pg_shdescription (objoid, classoid);
create TABLE pg_shseclabel (
    objoid oid not null,
    classoid oid not null,
    provider text,
    label text
);
CREATE UNIQUE INDEX pg_shseclabel_object_index ON pg_shseclabel (objoid, classoid, provider);
create TABLE pg_statistic (
    starelid oid not null,
    staattnum smallint not null,
    stainherit boolean not null,
    stanullfrac real not null,
    stawidth integer not null,
    stadistinct real not null,
    stakind1 smallint not null,
    stakind2 smallint not null,
    stakind3 smallint not null,
    stakind4 smallint not null,
    stakind5 smallint not null,
    staop1 oid not null,
    staop2 oid not null,
    staop3 oid not null,
    staop4 oid not null,
    staop5 oid not null,
    stanumbers1 real[],
    stanumbers2 real[],
    stanumbers3 real[],
    stanumbers4 real[],
    stanumbers5 real[],
    stavalues1 anyarray,
    stavalues2 anyarray,
    stavalues3 anyarray,
    stavalues4 anyarray,
    stavalues5 anyarray
);
CREATE UNIQUE INDEX pg_statistic_relid_att_inh_index ON pg_statistic (starelid, staattnum, stainherit);
create TABLE pg_tablespace (
    spcname name not null,
    spcowner oid not null,
    spcacl aclitem[],
    spcoptions text[]
);
CREATE UNIQUE INDEX pg_tablespace_spcname_index ON pg_tablespace (spcname);
CREATE UNIQUE INDEX pg_tablespace_oid_index ON pg_tablespace (oid);
create TABLE pg_trigger (
    tgrelid oid not null,
    tgname name not null,
    tgfoid oid not null,
    tgtype smallint not null,
    tgenabled "char" not null,
    tgisinternal boolean not null,
    tgconstrrelid oid not null,
    tgconstrindid oid not null,
    tgconstraint oid not null,
    tgdeferrable boolean not null,
    tginitdeferred boolean not null,
    tgnargs smallint not null,
    tgattr int2vector not null,
    tgargs bytea,
    tgqual pg_node_tree
);
CREATE UNIQUE INDEX pg_trigger_tgrelid_tgname_index ON pg_trigger (tgrelid, tgname);
CREATE UNIQUE INDEX pg_trigger_oid_index ON pg_trigger (oid);
CREATE INDEX pg_trigger_tgconstraint_index ON pg_trigger (tgconstraint);
create TABLE pg_ts_config (
    cfgname name not null,
    cfgnamespace oid not null,
    cfgowner oid not null,
    cfgparser oid not null
);
CREATE UNIQUE INDEX pg_ts_config_cfgname_index ON pg_ts_config (cfgname, cfgnamespace);
CREATE UNIQUE INDEX pg_ts_config_oid_index ON pg_ts_config (oid);
create TABLE pg_ts_config_map (
    mapcfg oid not null,
    maptokentype integer not null,
    mapseqno integer not null,
    mapdict oid not null
);
CREATE UNIQUE INDEX pg_ts_config_map_index ON pg_ts_config_map (mapcfg, maptokentype, mapseqno);
create TABLE pg_ts_dict (
    dictname name not null,
    dictnamespace oid not null,
    dictowner oid not null,
    dicttemplate oid not null,
    dictinitoption text
);
CREATE UNIQUE INDEX pg_ts_dict_dictname_index ON pg_ts_dict (dictname, dictnamespace);
CREATE UNIQUE INDEX pg_ts_dict_oid_index ON pg_ts_dict (oid);
create TABLE pg_ts_parser (
    prsname name not null,
    prsnamespace oid not null,
    prsstart regproc not null,
    prstoken regproc not null,
    prsend regproc not null,
    prsheadline regproc not null,
    prslextype regproc not null
);
CREATE UNIQUE INDEX pg_ts_parser_prsname_index ON pg_ts_parser (prsname, prsnamespace);
CREATE UNIQUE INDEX pg_ts_parser_oid_index ON pg_ts_parser (oid);
create TABLE pg_ts_template (
    tmplname name not null,
    tmplnamespace oid not null,
    tmplinit regproc not null,
    tmpllexize regproc not null
);
CREATE UNIQUE INDEX pg_ts_template_tmplname_index ON pg_ts_template (tmplname, tmplnamespace);
CREATE UNIQUE INDEX pg_ts_template_oid_index ON pg_ts_template (oid);
create TABLE pg_type (
    typname name not null,
    typnamespace oid not null,
    typowner oid not null,
    typlen smallint not null,
    typbyval boolean not null,
    typtype "char" not null,
    typcategory "char" not null,
    typispreferred boolean not null,
    typisdefined boolean not null,
    typdelim "char" not null,
    typrelid oid not null,
    typelem oid not null,
    typarray oid not null,
    typinput regproc not null,
    typoutput regproc not null,
    typreceive regproc not null,
    typsend regproc not null,
    typmodin regproc not null,
    typmodout regproc not null,
    typanalyze regproc not null,
    typalign "char" not null,
    typstorage "char" not null,
    typnotnull boolean not null,
    typbasetype oid not null,
    typtypmod integer not null,
    typndims integer not null,
    typcollation oid not null,
    typdefaultbin pg_node_tree,
    typdefault text,
    typacl aclitem[]
);
CREATE UNIQUE INDEX pg_type_typname_nsp_index ON pg_type (typname, typnamespace);
CREATE UNIQUE INDEX pg_type_oid_index ON pg_type (oid);
create TABLE pg_user_mapping (
    umuser oid not null,
    umserver oid not null,
    umoptions text[]
);
CREATE UNIQUE INDEX pg_user_mapping_user_server_index ON pg_user_mapping (umuser, umserver);
CREATE UNIQUE INDEX pg_user_mapping_oid_index ON pg_user_mapping (oid);
create VIEW pg_available_extension_versions (
    name name,
    version text,
    installed boolean,
    superuser boolean,
    relocatable boolean,
    "schema" name,
    requires name[],
    comment text
);
create VIEW pg_available_extensions (
    name name,
    default_version text,
    installed_version text,
    comment text
);
create VIEW pg_cursors (
    name text,
    statement text,
    is_holdable boolean,
    is_binary boolean,
    is_scrollable boolean,
    creation_time timestamp with time zone
);
create VIEW pg_group (
    groname name,
    grosysid oid,
    grolist oid[]
);
create VIEW pg_indexes (
    schemaname name,
    tablename name,
    indexname name,
    tablespace name,
    indexdef text
);
create VIEW pg_locks (
    locktype text,
    database oid,
    relation oid,
    page integer,
    tuple smallint,
    virtualxid text,
    transactionid xid,
    classid oid,
    objid oid,
    objsubid smallint,
    virtualtransaction text,
    pid integer,
    mode text,
    granted boolean,
    fastpath boolean
);
create VIEW pg_matviews (
    schemaname name,
    matviewname name,
    matviewowner name,
    tablespace name,
    hasindexes boolean,
    ispopulated boolean,
    definition text
);
create VIEW pg_prepared_statements (
    name text,
    statement text,
    prepare_time timestamp with time zone,
    parameter_types regtype[],
    from_sql boolean
);
create VIEW pg_prepared_xacts (
    "transaction" xid,
    gid text,
    prepared timestamp with time zone,
    owner name,
    database name
);
create VIEW pg_replication_slots (
    slot_name name,
    plugin name,
    slot_type text,
    datoid oid,
    database name,
    active boolean,
    xmin xid,
    catalog_xmin xid,
    restart_lsn pg_lsn
);
create VIEW pg_roles (
    rolname name,
    rolsuper boolean,
    rolinherit boolean,
    rolcreaterole boolean,
    rolcreatedb boolean,
    rolcatupdate boolean,
    rolcanlogin boolean,
    rolreplication boolean,
    rolconnlimit integer,
    rolpassword text,
    rolvaliduntil timestamp with time zone,
    rolconfig text[],
    oid oid
);
create VIEW pg_rules (
    schemaname name,
    tablename name,
    rulename name,
    definition text
);
create VIEW pg_seclabels (
    objoid oid,
    classoid oid,
    objsubid integer,
    objtype text,
    objnamespace oid,
    objname text,
    provider text,
    label text
);
create VIEW pg_settings (
    name text,
    setting text,
    unit text,
    category text,
    short_desc text,
    extra_desc text,
    context text,
    vartype text,
    source text,
    min_val text,
    max_val text,
    enumvals text[],
    boot_val text,
    reset_val text,
    sourcefile text,
    sourceline integer
);
create VIEW pg_shadow (
    usename name,
    usesysid oid,
    usecreatedb boolean,
    usesuper boolean,
    usecatupd boolean,
    userepl boolean,
    passwd text,
    valuntil abstime,
    useconfig text[]
);
create VIEW pg_stat_activity (
    datid oid,
    datname name,
    pid integer,
    usesysid oid,
    usename name,
    application_name text,
    client_addr inet,
    client_hostname text,
    client_port integer,
    backend_start timestamp with time zone,
    xact_start timestamp with time zone,
    query_start timestamp with time zone,
    state_change timestamp with time zone,
    waiting boolean,
    state text,
    backend_xid xid,
    backend_xmin xid,
    query text
);
create VIEW pg_stat_all_indexes (
    relid oid,
    indexrelid oid,
    schemaname name,
    relname name,
    indexrelname name,
    idx_scan bigint,
    idx_tup_read bigint,
    idx_tup_fetch bigint
);
create VIEW pg_stat_all_tables (
    relid oid,
    schemaname name,
    relname name,
    seq_scan bigint,
    seq_tup_read bigint,
    idx_scan bigint,
    idx_tup_fetch bigint,
    n_tup_ins bigint,
    n_tup_upd bigint,
    n_tup_del bigint,
    n_tup_hot_upd bigint,
    n_live_tup bigint,
    n_dead_tup bigint,
    n_mod_since_analyze bigint,
    last_vacuum timestamp with time zone,
    last_autovacuum timestamp with time zone,
    last_analyze timestamp with time zone,
    last_autoanalyze timestamp with time zone,
    vacuum_count bigint,
    autovacuum_count bigint,
    analyze_count bigint,
    autoanalyze_count bigint
);
create VIEW pg_stat_archiver (
    archived_count bigint,
    last_archived_wal text,
    last_archived_time timestamp with time zone,
    failed_count bigint,
    last_failed_wal text,
    last_failed_time timestamp with time zone,
    stats_reset timestamp with time zone
);
create VIEW pg_stat_bgwriter (
    checkpoints_timed bigint,
    checkpoints_req bigint,
    checkpoint_write_time double precision,
    checkpoint_sync_time double precision,
    buffers_checkpoint bigint,
    buffers_clean bigint,
    maxwritten_clean bigint,
    buffers_backend bigint,
    buffers_backend_fsync bigint,
    buffers_alloc bigint,
    stats_reset timestamp with time zone
);
create VIEW pg_stat_database (
    datid oid,
    datname name,
    numbackends integer,
    xact_commit bigint,
    xact_rollback bigint,
    blks_read bigint,
    blks_hit bigint,
    tup_returned bigint,
    tup_fetched bigint,
    tup_inserted bigint,
    tup_updated bigint,
    tup_deleted bigint,
    conflicts bigint,
    temp_files bigint,
    temp_bytes bigint,
    deadlocks bigint,
    blk_read_time double precision,
    blk_write_time double precision,
    stats_reset timestamp with time zone
);
create VIEW pg_stat_database_conflicts (
    datid oid,
    datname name,
    confl_tablespace bigint,
    confl_lock bigint,
    confl_snapshot bigint,
    confl_bufferpin bigint,
    confl_deadlock bigint
);
create VIEW pg_stat_replication (
    pid integer,
    usesysid oid,
    usename name,
    application_name text,
    client_addr inet,
    client_hostname text,
    client_port integer,
    backend_start timestamp with time zone,
    backend_xmin xid,
    state text,
    sent_location pg_lsn,
    write_location pg_lsn,
    flush_location pg_lsn,
    replay_location pg_lsn,
    sync_priority integer,
    sync_state text
);
create VIEW pg_stat_sys_indexes (
    relid oid,
    indexrelid oid,
    schemaname name,
    relname name,
    indexrelname name,
    idx_scan bigint,
    idx_tup_read bigint,
    idx_tup_fetch bigint
);
create VIEW pg_stat_sys_tables (
    relid oid,
    schemaname name,
    relname name,
    seq_scan bigint,
    seq_tup_read bigint,
    idx_scan bigint,
    idx_tup_fetch bigint,
    n_tup_ins bigint,
    n_tup_upd bigint,
    n_tup_del bigint,
    n_tup_hot_upd bigint,
    n_live_tup bigint,
    n_dead_tup bigint,
    n_mod_since_analyze bigint,
    last_vacuum timestamp with time zone,
    last_autovacuum timestamp with time zone,
    last_analyze timestamp with time zone,
    last_autoanalyze timestamp with time zone,
    vacuum_count bigint,
    autovacuum_count bigint,
    analyze_count bigint,
    autoanalyze_count bigint
);
create VIEW pg_stat_user_functions (
    funcid oid,
    schemaname name,
    funcname name,
    calls bigint,
    total_time double precision,
    self_time double precision
);
create VIEW pg_stat_user_indexes (
    relid oid,
    indexrelid oid,
    schemaname name,
    relname name,
    indexrelname name,
    idx_scan bigint,
    idx_tup_read bigint,
    idx_tup_fetch bigint
);
create VIEW pg_stat_user_tables (
    relid oid,
    schemaname name,
    relname name,
    seq_scan bigint,
    seq_tup_read bigint,
    idx_scan bigint,
    idx_tup_fetch bigint,
    n_tup_ins bigint,
    n_tup_upd bigint,
    n_tup_del bigint,
    n_tup_hot_upd bigint,
    n_live_tup bigint,
    n_dead_tup bigint,
    n_mod_since_analyze bigint,
    last_vacuum timestamp with time zone,
    last_autovacuum timestamp with time zone,
    last_analyze timestamp with time zone,
    last_autoanalyze timestamp with time zone,
    vacuum_count bigint,
    autovacuum_count bigint,
    analyze_count bigint,
    autoanalyze_count bigint
);
create VIEW pg_stat_xact_all_tables (
    relid oid,
    schemaname name,
    relname name,
    seq_scan bigint,
    seq_tup_read bigint,
    idx_scan bigint,
    idx_tup_fetch bigint,
    n_tup_ins bigint,
    n_tup_upd bigint,
    n_tup_del bigint,
    n_tup_hot_upd bigint
);
create VIEW pg_stat_xact_sys_tables (
    relid oid,
    schemaname name,
    relname name,
    seq_scan bigint,
    seq_tup_read bigint,
    idx_scan bigint,
    idx_tup_fetch bigint,
    n_tup_ins bigint,
    n_tup_upd bigint,
    n_tup_del bigint,
    n_tup_hot_upd bigint
);
create VIEW pg_stat_xact_user_functions (
    funcid oid,
    schemaname name,
    funcname name,
    calls bigint,
    total_time double precision,
    self_time double precision
);
create VIEW pg_stat_xact_user_tables (
    relid oid,
    schemaname name,
    relname name,
    seq_scan bigint,
    seq_tup_read bigint,
    idx_scan bigint,
    idx_tup_fetch bigint,
    n_tup_ins bigint,
    n_tup_upd bigint,
    n_tup_del bigint,
    n_tup_hot_upd bigint
);
create VIEW pg_statio_all_indexes (
    relid oid,
    indexrelid oid,
    schemaname name,
    relname name,
    indexrelname name,
    idx_blks_read bigint,
    idx_blks_hit bigint
);
create VIEW pg_statio_all_sequences (
    relid oid,
    schemaname name,
    relname name,
    blks_read bigint,
    blks_hit bigint
);
create VIEW pg_statio_all_tables (
    relid oid,
    schemaname name,
    relname name,
    heap_blks_read bigint,
    heap_blks_hit bigint,
    idx_blks_read bigint,
    idx_blks_hit bigint,
    toast_blks_read bigint,
    toast_blks_hit bigint,
    tidx_blks_read bigint,
    tidx_blks_hit bigint
);
create VIEW pg_statio_sys_indexes (
    relid oid,
    indexrelid oid,
    schemaname name,
    relname name,
    indexrelname name,
    idx_blks_read bigint,
    idx_blks_hit bigint
);
create VIEW pg_statio_sys_sequences (
    relid oid,
    schemaname name,
    relname name,
    blks_read bigint,
    blks_hit bigint
);
create VIEW pg_statio_sys_tables (
    relid oid,
    schemaname name,
    relname name,
    heap_blks_read bigint,
    heap_blks_hit bigint,
    idx_blks_read bigint,
    idx_blks_hit bigint,
    toast_blks_read bigint,
    toast_blks_hit bigint,
    tidx_blks_read bigint,
    tidx_blks_hit bigint
);
create VIEW pg_statio_user_indexes (
    relid oid,
    indexrelid oid,
    schemaname name,
    relname name,
    indexrelname name,
    idx_blks_read bigint,
    idx_blks_hit bigint
);
create VIEW pg_statio_user_sequences (
    relid oid,
    schemaname name,
    relname name,
    blks_read bigint,
    blks_hit bigint
);
create VIEW pg_statio_user_tables (
    relid oid,
    schemaname name,
    relname name,
    heap_blks_read bigint,
    heap_blks_hit bigint,
    idx_blks_read bigint,
    idx_blks_hit bigint,
    toast_blks_read bigint,
    toast_blks_hit bigint,
    tidx_blks_read bigint,
    tidx_blks_hit bigint
);
create VIEW pg_stats (
    schemaname name,
    tablename name,
    attname name,
    inherited boolean,
    null_frac real,
    avg_width integer,
    n_distinct real,
    most_common_vals anyarray,
    most_common_freqs real[],
    histogram_bounds anyarray,
    correlation real,
    most_common_elems anyarray,
    most_common_elem_freqs real[],
    elem_count_histogram real[]
);
create VIEW pg_tables (
    schemaname name,
    tablename name,
    tableowner name,
    tablespace name,
    hasindexes boolean,
    hasrules boolean,
    hastriggers boolean
);
create VIEW pg_timezone_abbrevs (
    abbrev text,
    utc_offset interval,
    is_dst boolean
);
create VIEW pg_timezone_names (
    name text,
    abbrev text,
    utc_offset interval,
    is_dst boolean
);
create VIEW pg_user (
    usename name,
    usesysid oid,
    usecreatedb boolean,
    usesuper boolean,
    usecatupd boolean,
    userepl boolean,
    passwd text,
    valuntil abstime,
    useconfig text[]
);
create VIEW pg_user_mappings (
    umid oid,
    srvid oid,
    srvname name,
    umuser oid,
    usename name,
    umoptions text[]
);
create VIEW pg_views (
    schemaname name,
    viewname name,
    viewowner name,
    definition text
);
create FUNCTION RI_FKey_cascade_del();
create FUNCTION RI_FKey_cascade_upd();
create FUNCTION RI_FKey_check_ins();
create FUNCTION RI_FKey_check_upd();
create FUNCTION RI_FKey_noaction_del();
create FUNCTION RI_FKey_noaction_upd();
create FUNCTION RI_FKey_restrict_del();
create FUNCTION RI_FKey_restrict_upd();
create FUNCTION RI_FKey_setdefault_del();
create FUNCTION RI_FKey_setdefault_upd();
create FUNCTION RI_FKey_setnull_del();
create FUNCTION RI_FKey_setnull_upd();
create FUNCTION abbrev($1 cidr) returns text;
create FUNCTION abbrev($1 inet) returns text;
create FUNCTION abs($1 bigint) returns bigint;
create FUNCTION abs($1 double precision) returns double precision;
create FUNCTION abs($1 integer) returns integer;
create FUNCTION abs($1 numeric) returns numeric;
create FUNCTION abs($1 real) returns real;
create FUNCTION abs($1 smallint) returns smallint;
create FUNCTION abstimeeq($1 abstime, $2 abstime) returns boolean;
create FUNCTION abstimege($1 abstime, $2 abstime) returns boolean;
create FUNCTION abstimegt($1 abstime, $2 abstime) returns boolean;
create FUNCTION abstimein($1 cstring) returns abstime;
create FUNCTION abstimele($1 abstime, $2 abstime) returns boolean;
create FUNCTION abstimelt($1 abstime, $2 abstime) returns boolean;
create FUNCTION abstimene($1 abstime, $2 abstime) returns boolean;
create FUNCTION abstimeout($1 abstime) returns cstring;
create FUNCTION abstimerecv($1 internal) returns abstime;
create FUNCTION abstimesend($1 abstime) returns bytea;
create FUNCTION abstime($1 timestamp) returns abstime;
create FUNCTION abstime($1 timestamp with time zone) returns abstime;
create FUNCTION aclcontains($1 aclitem[], $2 aclitem) returns boolean;
create FUNCTION acldefault($1 "char", $2 oid) returns aclitem[];
create FUNCTION aclexplode(acl aclitem[], grantor oid, grantee oid, privilege_type text, is_grantable boolean) returns setof record;
create FUNCTION aclinsert($1 aclitem[], $2 aclitem) returns aclitem[];
create FUNCTION aclitemeq($1 aclitem, $2 aclitem) returns boolean;
create FUNCTION aclitemin($1 cstring) returns aclitem;
create FUNCTION aclitemout($1 aclitem) returns cstring;
create FUNCTION aclremove($1 aclitem[], $2 aclitem) returns aclitem[];
create FUNCTION acos($1 double precision) returns double precision;
create FUNCTION age($1 timestamp) returns interval;
create FUNCTION age($1 timestamp with time zone) returns interval;
create FUNCTION age($1 timestamp with time zone, $2 timestamp with time zone) returns interval;
create FUNCTION age($1 timestamp, $2 timestamp) returns interval;
create FUNCTION age($1 xid) returns integer;
create FUNCTION any_in($1 cstring) returns "any";
create FUNCTION any_out($1 "any") returns cstring;
create FUNCTION anyarray_in($1 cstring) returns anyarray;
create FUNCTION anyarray_out($1 anyarray) returns cstring;
create FUNCTION anyarray_recv($1 internal) returns anyarray;
create FUNCTION anyarray_send($1 anyarray) returns bytea;
create FUNCTION anyelement_in($1 cstring) returns anyelement;
create FUNCTION anyelement_out($1 anyelement) returns cstring;
create FUNCTION anyenum_in($1 cstring) returns anyenum;
create FUNCTION anyenum_out($1 anyenum) returns cstring;
create FUNCTION anynonarray_in($1 cstring) returns anynonarray;
create FUNCTION anynonarray_out($1 anynonarray) returns cstring;
create FUNCTION anyrange_in($1 cstring, $2 oid, $3 integer) returns anyrange;
create FUNCTION anyrange_out($1 anyrange) returns cstring;
create FUNCTION anytextcat($1 anynonarray, $2 text) returns text;
create FUNCTION area($1 box) returns double precision;
create FUNCTION area($1 circle) returns double precision;
create FUNCTION areajoinsel($1 internal, $2 oid, $3 internal, $4 smallint, $5 internal) returns double precision;
create FUNCTION area($1 path) returns double precision;
create FUNCTION areasel($1 internal, $2 oid, $3 internal, $4 integer) returns double precision;
create FUNCTION array_agg_finalfn($1 internal, $2 anyelement) returns anyarray;
create FUNCTION array_agg_transfn($1 internal, $2 anyelement) returns internal;
create FUNCTION array_agg($1 anyelement) returns anyarray;
create FUNCTION array_append($1 anyarray, $2 anyelement) returns anyarray;
create FUNCTION array_cat($1 anyarray, $2 anyarray) returns anyarray;
create FUNCTION array_dims($1 anyarray) returns text;
create FUNCTION array_eq($1 anyarray, $2 anyarray) returns boolean;
create FUNCTION array_fill($1 anyelement, $2 integer[]) returns anyarray;
create FUNCTION array_fill($1 anyelement, $2 integer[], $3 integer[]) returns anyarray;
create FUNCTION array_ge($1 anyarray, $2 anyarray) returns boolean;
create FUNCTION array_gt($1 anyarray, $2 anyarray) returns boolean;
create FUNCTION array_in($1 cstring, $2 oid, $3 integer) returns anyarray;
create FUNCTION array_larger($1 anyarray, $2 anyarray) returns anyarray;
create FUNCTION array_le($1 anyarray, $2 anyarray) returns boolean;
create FUNCTION array_length($1 anyarray, $2 integer) returns integer;
create FUNCTION array_lower($1 anyarray, $2 integer) returns integer;
create FUNCTION array_lt($1 anyarray, $2 anyarray) returns boolean;
create FUNCTION array_ndims($1 anyarray) returns integer;
create FUNCTION array_ne($1 anyarray, $2 anyarray) returns boolean;
create FUNCTION array_out($1 anyarray) returns cstring;
create FUNCTION array_prepend($1 anyelement, $2 anyarray) returns anyarray;
create FUNCTION array_recv($1 internal, $2 oid, $3 integer) returns anyarray;
create FUNCTION array_remove($1 anyarray, $2 anyelement) returns anyarray;
create FUNCTION array_replace($1 anyarray, $2 anyelement, $3 anyelement) returns anyarray;
create FUNCTION array_send($1 anyarray) returns bytea;
create FUNCTION array_smaller($1 anyarray, $2 anyarray) returns anyarray;
create FUNCTION array_to_json($1 anyarray) returns json;
create FUNCTION array_to_json($1 anyarray, $2 boolean) returns json;
create FUNCTION array_to_string($1 anyarray, $2 text) returns text;
create FUNCTION array_to_string($1 anyarray, $2 text, $3 text) returns text;
create FUNCTION array_typanalyze($1 internal) returns boolean;
create FUNCTION array_upper($1 anyarray, $2 integer) returns integer;
create FUNCTION arraycontained($1 anyarray, $2 anyarray) returns boolean;
create FUNCTION arraycontains($1 anyarray, $2 anyarray) returns boolean;
create FUNCTION arraycontjoinsel($1 internal, $2 oid, $3 internal, $4 smallint, $5 internal) returns double precision;
create FUNCTION arraycontsel($1 internal, $2 oid, $3 internal, $4 integer) returns double precision;
create FUNCTION arrayoverlap($1 anyarray, $2 anyarray) returns boolean;
create FUNCTION ascii_to_mic($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION ascii_to_utf8($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION ascii($1 text) returns integer;
create FUNCTION asin($1 double precision) returns double precision;
create FUNCTION atan2($1 double precision, $2 double precision) returns double precision;
create FUNCTION atan($1 double precision) returns double precision;
create FUNCTION "avg"($1 bigint) returns numeric;
create FUNCTION "avg"($1 double precision) returns double precision;
create FUNCTION "avg"($1 integer) returns numeric;
create FUNCTION "avg"($1 interval) returns interval;
create FUNCTION "avg"($1 numeric) returns numeric;
create FUNCTION "avg"($1 real) returns double precision;
create FUNCTION "avg"($1 smallint) returns numeric;
create FUNCTION big5_to_euc_tw($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION big5_to_mic($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION big5_to_utf8($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION bit_and($1 bigint) returns bigint;
create FUNCTION bit_and($1 bit) returns bit;
create FUNCTION bit_and($1 integer) returns integer;
create FUNCTION bit_and($1 smallint) returns smallint;
create FUNCTION bit_in($1 cstring, $2 oid, $3 integer) returns bit;
create FUNCTION "bit_length"($1 bit) returns integer;
create FUNCTION "bit_length"($1 bytea) returns integer;
create FUNCTION "bit_length"($1 text) returns integer;
create FUNCTION bit_or($1 bigint) returns bigint;
create FUNCTION bit_or($1 bit) returns bit;
create FUNCTION bit_or($1 integer) returns integer;
create FUNCTION bit_or($1 smallint) returns smallint;
create FUNCTION bit_out($1 bit) returns cstring;
create FUNCTION bit_recv($1 internal, $2 oid, $3 integer) returns bit;
create FUNCTION bit_send($1 bit) returns bytea;
create FUNCTION bitand($1 bit, $2 bit) returns bit;
create FUNCTION "bit"($1 bigint, $2 integer) returns bit;
create FUNCTION "bit"($1 bit, $2 integer, $3 boolean) returns bit;
create FUNCTION bitcat($1 bit varying, $2 bit varying) returns bit varying;
create FUNCTION bitcmp($1 bit, $2 bit) returns integer;
create FUNCTION biteq($1 bit, $2 bit) returns boolean;
create FUNCTION bitge($1 bit, $2 bit) returns boolean;
create FUNCTION bitgt($1 bit, $2 bit) returns boolean;
create FUNCTION "bit"($1 integer, $2 integer) returns bit;
create FUNCTION bitle($1 bit, $2 bit) returns boolean;
create FUNCTION bitlt($1 bit, $2 bit) returns boolean;
create FUNCTION bitne($1 bit, $2 bit) returns boolean;
create FUNCTION bitnot($1 bit) returns bit;
create FUNCTION bitor($1 bit, $2 bit) returns bit;
create FUNCTION bitshiftleft($1 bit, $2 integer) returns bit;
create FUNCTION bitshiftright($1 bit, $2 integer) returns bit;
create FUNCTION bittypmodin($1 cstring[]) returns integer;
create FUNCTION bittypmodout($1 integer) returns cstring;
create FUNCTION bitxor($1 bit, $2 bit) returns bit;
create FUNCTION bool_accum_inv($1 internal, $2 boolean) returns internal;
create FUNCTION bool_accum($1 internal, $2 boolean) returns internal;
create FUNCTION bool_alltrue($1 internal) returns boolean;
create FUNCTION bool_and($1 boolean) returns boolean;
create FUNCTION bool_anytrue($1 internal) returns boolean;
create FUNCTION bool_or($1 boolean) returns boolean;
create FUNCTION booland_statefunc($1 boolean, $2 boolean) returns boolean;
create FUNCTION booleq($1 boolean, $2 boolean) returns boolean;
create FUNCTION boolge($1 boolean, $2 boolean) returns boolean;
create FUNCTION boolgt($1 boolean, $2 boolean) returns boolean;
create FUNCTION boolin($1 cstring) returns boolean;
create FUNCTION bool($1 integer) returns boolean;
create FUNCTION boolle($1 boolean, $2 boolean) returns boolean;
create FUNCTION boollt($1 boolean, $2 boolean) returns boolean;
create FUNCTION boolne($1 boolean, $2 boolean) returns boolean;
create FUNCTION boolor_statefunc($1 boolean, $2 boolean) returns boolean;
create FUNCTION boolout($1 boolean) returns cstring;
create FUNCTION boolrecv($1 internal) returns boolean;
create FUNCTION boolsend($1 boolean) returns bytea;
create FUNCTION box_above_eq($1 box, $2 box) returns boolean;
create FUNCTION box_above($1 box, $2 box) returns boolean;
create FUNCTION box_add($1 box, $2 point) returns box;
create FUNCTION box_below_eq($1 box, $2 box) returns boolean;
create FUNCTION box_below($1 box, $2 box) returns boolean;
create FUNCTION box_center($1 box) returns point;
create FUNCTION box_contain_pt($1 box, $2 point) returns boolean;
create FUNCTION box_contain($1 box, $2 box) returns boolean;
create FUNCTION box_contained($1 box, $2 box) returns boolean;
create FUNCTION box_distance($1 box, $2 box) returns double precision;
create FUNCTION box_div($1 box, $2 point) returns box;
create FUNCTION box_eq($1 box, $2 box) returns boolean;
create FUNCTION box_ge($1 box, $2 box) returns boolean;
create FUNCTION box_gt($1 box, $2 box) returns boolean;
create FUNCTION box_in($1 cstring) returns box;
create FUNCTION box_intersect($1 box, $2 box) returns box;
create FUNCTION box_le($1 box, $2 box) returns boolean;
create FUNCTION box_left($1 box, $2 box) returns boolean;
create FUNCTION box_lt($1 box, $2 box) returns boolean;
create FUNCTION box_mul($1 box, $2 point) returns box;
create FUNCTION box_out($1 box) returns cstring;
create FUNCTION box_overabove($1 box, $2 box) returns boolean;
create FUNCTION box_overbelow($1 box, $2 box) returns boolean;
create FUNCTION box_overlap($1 box, $2 box) returns boolean;
create FUNCTION box_overleft($1 box, $2 box) returns boolean;
create FUNCTION box_overright($1 box, $2 box) returns boolean;
create FUNCTION box_recv($1 internal) returns box;
create FUNCTION box_right($1 box, $2 box) returns boolean;
create FUNCTION box_same($1 box, $2 box) returns boolean;
create FUNCTION box_send($1 box) returns bytea;
create FUNCTION box_sub($1 box, $2 point) returns box;
create FUNCTION box($1 circle) returns box;
create FUNCTION box($1 point, $2 point) returns box;
create FUNCTION box($1 polygon) returns box;
create FUNCTION bpchar($1 "char") returns char;
create FUNCTION bpchar_larger($1 char, $2 char) returns char;
create FUNCTION bpchar_pattern_ge($1 char, $2 char) returns boolean;
create FUNCTION bpchar_pattern_gt($1 char, $2 char) returns boolean;
create FUNCTION bpchar_pattern_le($1 char, $2 char) returns boolean;
create FUNCTION bpchar_pattern_lt($1 char, $2 char) returns boolean;
create FUNCTION bpchar_smaller($1 char, $2 char) returns char;
create FUNCTION bpchar($1 char, $2 integer, $3 boolean) returns char;
create FUNCTION bpcharcmp($1 char, $2 char) returns integer;
create FUNCTION bpchareq($1 char, $2 char) returns boolean;
create FUNCTION bpcharge($1 char, $2 char) returns boolean;
create FUNCTION bpchargt($1 char, $2 char) returns boolean;
create FUNCTION bpchariclike($1 char, $2 text) returns boolean;
create FUNCTION bpcharicnlike($1 char, $2 text) returns boolean;
create FUNCTION bpcharicregexeq($1 char, $2 text) returns boolean;
create FUNCTION bpcharicregexne($1 char, $2 text) returns boolean;
create FUNCTION bpcharin($1 cstring, $2 oid, $3 integer) returns char;
create FUNCTION bpcharle($1 char, $2 char) returns boolean;
create FUNCTION bpcharlike($1 char, $2 text) returns boolean;
create FUNCTION bpcharlt($1 char, $2 char) returns boolean;
create FUNCTION bpchar($1 name) returns char;
create FUNCTION bpcharne($1 char, $2 char) returns boolean;
create FUNCTION bpcharnlike($1 char, $2 text) returns boolean;
create FUNCTION bpcharout($1 char) returns cstring;
create FUNCTION bpcharrecv($1 internal, $2 oid, $3 integer) returns char;
create FUNCTION bpcharregexeq($1 char, $2 text) returns boolean;
create FUNCTION bpcharregexne($1 char, $2 text) returns boolean;
create FUNCTION bpcharsend($1 char) returns bytea;
create FUNCTION bpchartypmodin($1 cstring[]) returns integer;
create FUNCTION bpchartypmodout($1 integer) returns cstring;
create FUNCTION broadcast($1 inet) returns inet;
create FUNCTION btabstimecmp($1 abstime, $2 abstime) returns integer;
create FUNCTION btarraycmp($1 anyarray, $2 anyarray) returns integer;
create FUNCTION btbeginscan($1 internal, $2 internal, $3 internal) returns internal;
create FUNCTION btboolcmp($1 boolean, $2 boolean) returns integer;
create FUNCTION btbpchar_pattern_cmp($1 char, $2 char) returns integer;
create FUNCTION btbuildempty($1 internal) returns void;
create FUNCTION btbuild($1 internal, $2 internal, $3 internal) returns internal;
create FUNCTION btbulkdelete($1 internal, $2 internal, $3 internal, $4 internal) returns internal;
create FUNCTION btcanreturn($1 internal) returns boolean;
create FUNCTION btcharcmp($1 "char", $2 "char") returns integer;
create FUNCTION btcostestimate($1 internal, $2 internal, $3 internal, $4 internal, $5 internal, $6 internal, $7 internal) returns void;
create FUNCTION btendscan($1 internal) returns void;
create FUNCTION btfloat48cmp($1 real, $2 double precision) returns integer;
create FUNCTION btfloat4cmp($1 real, $2 real) returns integer;
create FUNCTION btfloat4sortsupport($1 internal) returns void;
create FUNCTION btfloat84cmp($1 double precision, $2 real) returns integer;
create FUNCTION btfloat8cmp($1 double precision, $2 double precision) returns integer;
create FUNCTION btfloat8sortsupport($1 internal) returns void;
create FUNCTION btgetbitmap($1 internal, $2 internal) returns bigint;
create FUNCTION btgettuple($1 internal, $2 internal) returns boolean;
create FUNCTION btinsert($1 internal, $2 internal, $3 internal, $4 internal, $5 internal, $6 internal) returns boolean;
create FUNCTION btint24cmp($1 smallint, $2 integer) returns integer;
create FUNCTION btint28cmp($1 smallint, $2 bigint) returns integer;
create FUNCTION btint2cmp($1 smallint, $2 smallint) returns integer;
create FUNCTION btint2sortsupport($1 internal) returns void;
create FUNCTION btint42cmp($1 integer, $2 smallint) returns integer;
create FUNCTION btint48cmp($1 integer, $2 bigint) returns integer;
create FUNCTION btint4cmp($1 integer, $2 integer) returns integer;
create FUNCTION btint4sortsupport($1 internal) returns void;
create FUNCTION btint82cmp($1 bigint, $2 smallint) returns integer;
create FUNCTION btint84cmp($1 bigint, $2 integer) returns integer;
create FUNCTION btint8cmp($1 bigint, $2 bigint) returns integer;
create FUNCTION btint8sortsupport($1 internal) returns void;
create FUNCTION btmarkpos($1 internal) returns void;
create FUNCTION btnamecmp($1 name, $2 name) returns integer;
create FUNCTION btnamesortsupport($1 internal) returns void;
create FUNCTION btoidcmp($1 oid, $2 oid) returns integer;
create FUNCTION btoidsortsupport($1 internal) returns void;
create FUNCTION btoidvectorcmp($1 oidvector, $2 oidvector) returns integer;
create FUNCTION btoptions($1 text[], $2 boolean) returns bytea;
create FUNCTION btrecordcmp($1 record, $2 record) returns integer;
create FUNCTION btrecordimagecmp($1 record, $2 record) returns integer;
create FUNCTION btreltimecmp($1 reltime, $2 reltime) returns integer;
create FUNCTION btrescan($1 internal, $2 internal, $3 internal, $4 internal, $5 internal) returns void;
create FUNCTION btrestrpos($1 internal) returns void;
create FUNCTION btrim($1 bytea, $2 bytea) returns bytea;
create FUNCTION btrim($1 text) returns text;
create FUNCTION btrim($1 text, $2 text) returns text;
create FUNCTION bttext_pattern_cmp($1 text, $2 text) returns integer;
create FUNCTION bttextcmp($1 text, $2 text) returns integer;
create FUNCTION bttidcmp($1 tid, $2 tid) returns integer;
create FUNCTION bttintervalcmp($1 tinterval, $2 tinterval) returns integer;
create FUNCTION btvacuumcleanup($1 internal, $2 internal) returns internal;
create FUNCTION bytea_string_agg_finalfn($1 internal) returns bytea;
create FUNCTION bytea_string_agg_transfn($1 internal, $2 bytea, $3 bytea) returns internal;
create FUNCTION byteacat($1 bytea, $2 bytea) returns bytea;
create FUNCTION byteacmp($1 bytea, $2 bytea) returns integer;
create FUNCTION byteaeq($1 bytea, $2 bytea) returns boolean;
create FUNCTION byteage($1 bytea, $2 bytea) returns boolean;
create FUNCTION byteagt($1 bytea, $2 bytea) returns boolean;
create FUNCTION byteain($1 cstring) returns bytea;
create FUNCTION byteale($1 bytea, $2 bytea) returns boolean;
create FUNCTION bytealike($1 bytea, $2 bytea) returns boolean;
create FUNCTION bytealt($1 bytea, $2 bytea) returns boolean;
create FUNCTION byteane($1 bytea, $2 bytea) returns boolean;
create FUNCTION byteanlike($1 bytea, $2 bytea) returns boolean;
create FUNCTION byteaout($1 bytea) returns cstring;
create FUNCTION bytearecv($1 internal) returns bytea;
create FUNCTION byteasend($1 bytea) returns bytea;
create FUNCTION cardinality($1 anyarray) returns integer;
create FUNCTION cash_cmp($1 money, $2 money) returns integer;
create FUNCTION cash_div_cash($1 money, $2 money) returns double precision;
create FUNCTION cash_div_flt4($1 money, $2 real) returns money;
create FUNCTION cash_div_flt8($1 money, $2 double precision) returns money;
create FUNCTION cash_div_int2($1 money, $2 smallint) returns money;
create FUNCTION cash_div_int4($1 money, $2 integer) returns money;
create FUNCTION cash_eq($1 money, $2 money) returns boolean;
create FUNCTION cash_ge($1 money, $2 money) returns boolean;
create FUNCTION cash_gt($1 money, $2 money) returns boolean;
create FUNCTION cash_in($1 cstring) returns money;
create FUNCTION cash_le($1 money, $2 money) returns boolean;
create FUNCTION cash_lt($1 money, $2 money) returns boolean;
create FUNCTION cash_mi($1 money, $2 money) returns money;
create FUNCTION cash_mul_flt4($1 money, $2 real) returns money;
create FUNCTION cash_mul_flt8($1 money, $2 double precision) returns money;
create FUNCTION cash_mul_int2($1 money, $2 smallint) returns money;
create FUNCTION cash_mul_int4($1 money, $2 integer) returns money;
create FUNCTION cash_ne($1 money, $2 money) returns boolean;
create FUNCTION cash_out($1 money) returns cstring;
create FUNCTION cash_pl($1 money, $2 money) returns money;
create FUNCTION cash_recv($1 internal) returns money;
create FUNCTION cash_send($1 money) returns bytea;
create FUNCTION cash_words($1 money) returns text;
create FUNCTION cashlarger($1 money, $2 money) returns money;
create FUNCTION cashsmaller($1 money, $2 money) returns money;
create FUNCTION cbrt($1 double precision) returns double precision;
create FUNCTION ceil($1 double precision) returns double precision;
create FUNCTION ceiling($1 double precision) returns double precision;
create FUNCTION ceiling($1 numeric) returns numeric;
create FUNCTION ceil($1 numeric) returns numeric;
create FUNCTION center($1 box) returns point;
create FUNCTION center($1 circle) returns point;
create FUNCTION "char_length"($1 char) returns integer;
create FUNCTION "char_length"($1 text) returns integer;
create FUNCTION "character_length"($1 char) returns integer;
create FUNCTION "character_length"($1 text) returns integer;
create FUNCTION chareq($1 "char", $2 "char") returns boolean;
create FUNCTION charge($1 "char", $2 "char") returns boolean;
create FUNCTION chargt($1 "char", $2 "char") returns boolean;
create FUNCTION charin($1 cstring) returns "char";
create FUNCTION "char"($1 integer) returns "char";
create FUNCTION charle($1 "char", $2 "char") returns boolean;
create FUNCTION charlt($1 "char", $2 "char") returns boolean;
create FUNCTION charne($1 "char", $2 "char") returns boolean;
create FUNCTION charout($1 "char") returns cstring;
create FUNCTION charrecv($1 internal) returns "char";
create FUNCTION charsend($1 "char") returns bytea;
create FUNCTION "char"($1 text) returns "char";
create FUNCTION chr($1 integer) returns text;
create FUNCTION cideq($1 cid, $2 cid) returns boolean;
create FUNCTION cidin($1 cstring) returns cid;
create FUNCTION cidout($1 cid) returns cstring;
create FUNCTION cidr_in($1 cstring) returns cidr;
create FUNCTION cidr_out($1 cidr) returns cstring;
create FUNCTION cidr_recv($1 internal) returns cidr;
create FUNCTION cidr_send($1 cidr) returns bytea;
create FUNCTION cidrecv($1 internal) returns cid;
create FUNCTION cidr($1 inet) returns cidr;
create FUNCTION cidsend($1 cid) returns bytea;
create FUNCTION circle_above($1 circle, $2 circle) returns boolean;
create FUNCTION circle_add_pt($1 circle, $2 point) returns circle;
create FUNCTION circle_below($1 circle, $2 circle) returns boolean;
create FUNCTION circle_center($1 circle) returns point;
create FUNCTION circle_contain_pt($1 circle, $2 point) returns boolean;
create FUNCTION circle_contain($1 circle, $2 circle) returns boolean;
create FUNCTION circle_contained($1 circle, $2 circle) returns boolean;
create FUNCTION circle_distance($1 circle, $2 circle) returns double precision;
create FUNCTION circle_div_pt($1 circle, $2 point) returns circle;
create FUNCTION circle_eq($1 circle, $2 circle) returns boolean;
create FUNCTION circle_ge($1 circle, $2 circle) returns boolean;
create FUNCTION circle_gt($1 circle, $2 circle) returns boolean;
create FUNCTION circle_in($1 cstring) returns circle;
create FUNCTION circle_le($1 circle, $2 circle) returns boolean;
create FUNCTION circle_left($1 circle, $2 circle) returns boolean;
create FUNCTION circle_lt($1 circle, $2 circle) returns boolean;
create FUNCTION circle_mul_pt($1 circle, $2 point) returns circle;
create FUNCTION circle_ne($1 circle, $2 circle) returns boolean;
create FUNCTION circle_out($1 circle) returns cstring;
create FUNCTION circle_overabove($1 circle, $2 circle) returns boolean;
create FUNCTION circle_overbelow($1 circle, $2 circle) returns boolean;
create FUNCTION circle_overlap($1 circle, $2 circle) returns boolean;
create FUNCTION circle_overleft($1 circle, $2 circle) returns boolean;
create FUNCTION circle_overright($1 circle, $2 circle) returns boolean;
create FUNCTION circle_recv($1 internal) returns circle;
create FUNCTION circle_right($1 circle, $2 circle) returns boolean;
create FUNCTION circle_same($1 circle, $2 circle) returns boolean;
create FUNCTION circle_send($1 circle) returns bytea;
create FUNCTION circle_sub_pt($1 circle, $2 point) returns circle;
create FUNCTION circle($1 box) returns circle;
create FUNCTION circle($1 point, $2 double precision) returns circle;
create FUNCTION circle($1 polygon) returns circle;
create FUNCTION clock_timestamp();
create FUNCTION close_lb($1 line, $2 box) returns point;
create FUNCTION close_lseg($1 lseg, $2 lseg) returns point;
create FUNCTION close_ls($1 line, $2 lseg) returns point;
create FUNCTION close_pb($1 point, $2 box) returns point;
create FUNCTION close_pl($1 point, $2 line) returns point;
create FUNCTION close_ps($1 point, $2 lseg) returns point;
create FUNCTION close_sb($1 lseg, $2 box) returns point;
create FUNCTION close_sl($1 lseg, $2 line) returns point;
create FUNCTION col_description($1 oid, $2 integer) returns text;
create FUNCTION concat($1 "any") returns text;
create FUNCTION concat_ws($1 text, $2 "any") returns text;
create FUNCTION contjoinsel($1 internal, $2 oid, $3 internal, $4 smallint, $5 internal) returns double precision;
create FUNCTION contsel($1 internal, $2 oid, $3 internal, $4 integer) returns double precision;
create FUNCTION convert_from($1 bytea, $2 name) returns text;
create FUNCTION convert_to($1 text, $2 name) returns bytea;
create FUNCTION "convert"($1 bytea, $2 name, $3 name) returns bytea;
create FUNCTION corr($1 double precision, $2 double precision) returns double precision;
create FUNCTION cos($1 double precision) returns double precision;
create FUNCTION cot($1 double precision) returns double precision;
create FUNCTION "count"();
create FUNCTION "count"($1 "any") returns bigint;
create FUNCTION covar_pop($1 double precision, $2 double precision) returns double precision;
create FUNCTION covar_samp($1 double precision, $2 double precision) returns double precision;
create FUNCTION cstring_in($1 cstring) returns cstring;
create FUNCTION cstring_out($1 cstring) returns cstring;
create FUNCTION cstring_recv($1 internal) returns cstring;
create FUNCTION cstring_send($1 cstring) returns bytea;
create FUNCTION cume_dist();
create FUNCTION cume_dist($1 "any") returns double precision;
create FUNCTION cume_dist_final($1 internal, $2 "any") returns double precision;
create FUNCTION current_database();
create FUNCTION current_query();
create FUNCTION current_schema();
create FUNCTION current_schemas($1 boolean) returns name[];
create FUNCTION current_setting($1 text) returns text;
create FUNCTION "current_user"();
create FUNCTION currtid2($1 text, $2 tid) returns tid;
create FUNCTION currtid($1 oid, $2 tid) returns tid;
create FUNCTION currval($1 regclass) returns bigint;
create FUNCTION cursor_to_xml("cursor" refcursor, "count" integer, nulls boolean, tableforest boolean, targetns text) returns xml;
create FUNCTION cursor_to_xmlschema("cursor" refcursor, nulls boolean, tableforest boolean, targetns text) returns xml;
create FUNCTION database_to_xml_and_xmlschema(nulls boolean, tableforest boolean, targetns text) returns xml;
create FUNCTION database_to_xml(nulls boolean, tableforest boolean, targetns text) returns xml;
create FUNCTION database_to_xmlschema(nulls boolean, tableforest boolean, targetns text) returns xml;
create FUNCTION date_cmp_timestamp($1 date, $2 timestamp) returns integer;
create FUNCTION date_cmp_timestamptz($1 date, $2 timestamp with time zone) returns integer;
create FUNCTION date_cmp($1 date, $2 date) returns integer;
create FUNCTION date_eq_timestamp($1 date, $2 timestamp) returns boolean;
create FUNCTION date_eq_timestamptz($1 date, $2 timestamp with time zone) returns boolean;
create FUNCTION date_eq($1 date, $2 date) returns boolean;
create FUNCTION date_ge_timestamp($1 date, $2 timestamp) returns boolean;
create FUNCTION date_ge_timestamptz($1 date, $2 timestamp with time zone) returns boolean;
create FUNCTION date_ge($1 date, $2 date) returns boolean;
create FUNCTION date_gt_timestamp($1 date, $2 timestamp) returns boolean;
create FUNCTION date_gt_timestamptz($1 date, $2 timestamp with time zone) returns boolean;
create FUNCTION date_gt($1 date, $2 date) returns boolean;
create FUNCTION date_in($1 cstring) returns date;
create FUNCTION date_larger($1 date, $2 date) returns date;
create FUNCTION date_le_timestamp($1 date, $2 timestamp) returns boolean;
create FUNCTION date_le_timestamptz($1 date, $2 timestamp with time zone) returns boolean;
create FUNCTION date_le($1 date, $2 date) returns boolean;
create FUNCTION date_lt_timestamp($1 date, $2 timestamp) returns boolean;
create FUNCTION date_lt_timestamptz($1 date, $2 timestamp with time zone) returns boolean;
create FUNCTION date_lt($1 date, $2 date) returns boolean;
create FUNCTION date_mi_interval($1 date, $2 interval) returns timestamp;
create FUNCTION date_mi($1 date, $2 date) returns integer;
create FUNCTION date_mii($1 date, $2 integer) returns date;
create FUNCTION date_ne_timestamp($1 date, $2 timestamp) returns boolean;
create FUNCTION date_ne_timestamptz($1 date, $2 timestamp with time zone) returns boolean;
create FUNCTION date_ne($1 date, $2 date) returns boolean;
create FUNCTION date_out($1 date) returns cstring;
create FUNCTION date_part($1 text, $2 abstime) returns double precision;
create FUNCTION date_part($1 text, $2 date) returns double precision;
create FUNCTION date_part($1 text, $2 interval) returns double precision;
create FUNCTION date_part($1 text, $2 reltime) returns double precision;
create FUNCTION date_part($1 text, $2 time) returns double precision;
create FUNCTION date_part($1 text, $2 time with time zone) returns double precision;
create FUNCTION date_part($1 text, $2 timestamp) returns double precision;
create FUNCTION date_part($1 text, $2 timestamp with time zone) returns double precision;
create FUNCTION date_pl_interval($1 date, $2 interval) returns timestamp;
create FUNCTION date_pli($1 date, $2 integer) returns date;
create FUNCTION date_recv($1 internal) returns date;
create FUNCTION date_send($1 date) returns bytea;
create FUNCTION date_smaller($1 date, $2 date) returns date;
create FUNCTION date_sortsupport($1 internal) returns void;
create FUNCTION date_trunc($1 text, $2 interval) returns interval;
create FUNCTION date_trunc($1 text, $2 timestamp) returns timestamp;
create FUNCTION date_trunc($1 text, $2 timestamp with time zone) returns timestamp with time zone;
create FUNCTION "date"($1 abstime) returns date;
create FUNCTION daterange_canonical($1 daterange) returns daterange;
create FUNCTION daterange_subdiff($1 date, $2 date) returns double precision;
create FUNCTION daterange($1 date, $2 date) returns daterange;
create FUNCTION daterange($1 date, $2 date, $3 text) returns daterange;
create FUNCTION datetime_pl($1 date, $2 time) returns timestamp;
create FUNCTION "date"($1 timestamp) returns date;
create FUNCTION "date"($1 timestamp with time zone) returns date;
create FUNCTION datetimetz_pl($1 date, $2 time with time zone) returns timestamp with time zone;
create FUNCTION dcbrt($1 double precision) returns double precision;
create FUNCTION decode($1 text, $2 text) returns bytea;
create FUNCTION degrees($1 double precision) returns double precision;
create FUNCTION dense_rank();
create FUNCTION dense_rank($1 "any") returns bigint;
create FUNCTION dense_rank_final($1 internal, $2 "any") returns bigint;
create FUNCTION dexp($1 double precision) returns double precision;
create FUNCTION diagonal($1 box) returns lseg;
create FUNCTION diameter($1 circle) returns double precision;
create FUNCTION dispell_init($1 internal) returns internal;
create FUNCTION dispell_lexize($1 internal, $2 internal, $3 internal, $4 internal) returns internal;
create FUNCTION dist_cpoly($1 circle, $2 polygon) returns double precision;
create FUNCTION dist_lb($1 line, $2 box) returns double precision;
create FUNCTION dist_pb($1 point, $2 box) returns double precision;
create FUNCTION dist_pc($1 point, $2 circle) returns double precision;
create FUNCTION dist_pl($1 point, $2 line) returns double precision;
create FUNCTION dist_ppath($1 point, $2 path) returns double precision;
create FUNCTION dist_ps($1 point, $2 lseg) returns double precision;
create FUNCTION dist_sb($1 lseg, $2 box) returns double precision;
create FUNCTION dist_sl($1 lseg, $2 line) returns double precision;
create FUNCTION div($1 numeric, $2 numeric) returns numeric;
create FUNCTION dlog10($1 double precision) returns double precision;
create FUNCTION dlog1($1 double precision) returns double precision;
create FUNCTION domain_in($1 cstring, $2 oid, $3 integer) returns "any";
create FUNCTION domain_recv($1 internal, $2 oid, $3 integer) returns "any";
create FUNCTION dpow($1 double precision, $2 double precision) returns double precision;
create FUNCTION dround($1 double precision) returns double precision;
create FUNCTION dsimple_init($1 internal) returns internal;
create FUNCTION dsimple_lexize($1 internal, $2 internal, $3 internal, $4 internal) returns internal;
create FUNCTION dsnowball_init($1 internal) returns internal;
create FUNCTION dsnowball_lexize($1 internal, $2 internal, $3 internal, $4 internal) returns internal;
create FUNCTION dsqrt($1 double precision) returns double precision;
create FUNCTION dsynonym_init($1 internal) returns internal;
create FUNCTION dsynonym_lexize($1 internal, $2 internal, $3 internal, $4 internal) returns internal;
create FUNCTION dtrunc($1 double precision) returns double precision;
create FUNCTION elem_contained_by_range($1 anyelement, $2 anyrange) returns boolean;
create FUNCTION encode($1 bytea, $2 text) returns text;
create FUNCTION enum_cmp($1 anyenum, $2 anyenum) returns integer;
create FUNCTION enum_eq($1 anyenum, $2 anyenum) returns boolean;
create FUNCTION enum_first($1 anyenum) returns anyenum;
create FUNCTION enum_ge($1 anyenum, $2 anyenum) returns boolean;
create FUNCTION enum_gt($1 anyenum, $2 anyenum) returns boolean;
create FUNCTION enum_in($1 cstring, $2 oid) returns anyenum;
create FUNCTION enum_larger($1 anyenum, $2 anyenum) returns anyenum;
create FUNCTION enum_last($1 anyenum) returns anyenum;
create FUNCTION enum_le($1 anyenum, $2 anyenum) returns boolean;
create FUNCTION enum_lt($1 anyenum, $2 anyenum) returns boolean;
create FUNCTION enum_ne($1 anyenum, $2 anyenum) returns boolean;
create FUNCTION enum_out($1 anyenum) returns cstring;
create FUNCTION enum_range($1 anyenum) returns anyarray;
create FUNCTION enum_range($1 anyenum, $2 anyenum) returns anyarray;
create FUNCTION enum_recv($1 internal, $2 oid) returns anyenum;
create FUNCTION enum_send($1 anyenum) returns bytea;
create FUNCTION enum_smaller($1 anyenum, $2 anyenum) returns anyenum;
create FUNCTION eqjoinsel($1 internal, $2 oid, $3 internal, $4 smallint, $5 internal) returns double precision;
create FUNCTION eqsel($1 internal, $2 oid, $3 internal, $4 integer) returns double precision;
create FUNCTION euc_cn_to_mic($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION euc_cn_to_utf8($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION euc_jis_2004_to_shift_jis_2004($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION euc_jis_2004_to_utf8($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION euc_jp_to_mic($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION euc_jp_to_sjis($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION euc_jp_to_utf8($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION euc_kr_to_mic($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION euc_kr_to_utf8($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION euc_tw_to_big5($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION euc_tw_to_mic($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION euc_tw_to_utf8($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION event_trigger_in($1 cstring) returns event_trigger;
create FUNCTION event_trigger_out($1 event_trigger) returns cstring;
create FUNCTION every($1 boolean) returns boolean;
create FUNCTION exp($1 double precision) returns double precision;
create FUNCTION exp($1 numeric) returns numeric;
create FUNCTION factorial($1 bigint) returns numeric;
create FUNCTION family($1 inet) returns integer;
create FUNCTION fdw_handler_in($1 cstring) returns fdw_handler;
create FUNCTION fdw_handler_out($1 fdw_handler) returns cstring;
create FUNCTION first_value($1 anyelement) returns anyelement;
create FUNCTION float48div($1 real, $2 double precision) returns double precision;
create FUNCTION float48eq($1 real, $2 double precision) returns boolean;
create FUNCTION float48ge($1 real, $2 double precision) returns boolean;
create FUNCTION float48gt($1 real, $2 double precision) returns boolean;
create FUNCTION float48le($1 real, $2 double precision) returns boolean;
create FUNCTION float48lt($1 real, $2 double precision) returns boolean;
create FUNCTION float48mi($1 real, $2 double precision) returns double precision;
create FUNCTION float48mul($1 real, $2 double precision) returns double precision;
create FUNCTION float48ne($1 real, $2 double precision) returns boolean;
create FUNCTION float48pl($1 real, $2 double precision) returns double precision;
create FUNCTION float4_accum($1 double precision[], $2 real) returns double precision[];
create FUNCTION float4abs($1 real) returns real;
create FUNCTION float4($1 bigint) returns real;
create FUNCTION float4div($1 real, $2 real) returns real;
create FUNCTION float4($1 double precision) returns real;
create FUNCTION float4eq($1 real, $2 real) returns boolean;
create FUNCTION float4ge($1 real, $2 real) returns boolean;
create FUNCTION float4gt($1 real, $2 real) returns boolean;
create FUNCTION float4in($1 cstring) returns real;
create FUNCTION float4($1 integer) returns real;
create FUNCTION float4larger($1 real, $2 real) returns real;
create FUNCTION float4le($1 real, $2 real) returns boolean;
create FUNCTION float4lt($1 real, $2 real) returns boolean;
create FUNCTION float4mi($1 real, $2 real) returns real;
create FUNCTION float4mul($1 real, $2 real) returns real;
create FUNCTION float4ne($1 real, $2 real) returns boolean;
create FUNCTION float4($1 numeric) returns real;
create FUNCTION float4out($1 real) returns cstring;
create FUNCTION float4pl($1 real, $2 real) returns real;
create FUNCTION float4recv($1 internal) returns real;
create FUNCTION float4send($1 real) returns bytea;
create FUNCTION float4smaller($1 real, $2 real) returns real;
create FUNCTION float4($1 smallint) returns real;
create FUNCTION float4um($1 real) returns real;
create FUNCTION float4up($1 real) returns real;
create FUNCTION float84div($1 double precision, $2 real) returns double precision;
create FUNCTION float84eq($1 double precision, $2 real) returns boolean;
create FUNCTION float84ge($1 double precision, $2 real) returns boolean;
create FUNCTION float84gt($1 double precision, $2 real) returns boolean;
create FUNCTION float84le($1 double precision, $2 real) returns boolean;
create FUNCTION float84lt($1 double precision, $2 real) returns boolean;
create FUNCTION float84mi($1 double precision, $2 real) returns double precision;
create FUNCTION float84mul($1 double precision, $2 real) returns double precision;
create FUNCTION float84ne($1 double precision, $2 real) returns boolean;
create FUNCTION float84pl($1 double precision, $2 real) returns double precision;
create FUNCTION float8_accum($1 double precision[], $2 double precision) returns double precision[];
create FUNCTION float8_avg($1 double precision[]) returns double precision;
create FUNCTION float8_corr($1 double precision[]) returns double precision;
create FUNCTION float8_covar_pop($1 double precision[]) returns double precision;
create FUNCTION float8_covar_samp($1 double precision[]) returns double precision;
create FUNCTION float8_regr_accum($1 double precision[], $2 double precision, $3 double precision) returns double precision[];
create FUNCTION float8_regr_avgx($1 double precision[]) returns double precision;
create FUNCTION float8_regr_avgy($1 double precision[]) returns double precision;
create FUNCTION float8_regr_intercept($1 double precision[]) returns double precision;
create FUNCTION float8_regr_r2($1 double precision[]) returns double precision;
create FUNCTION float8_regr_slope($1 double precision[]) returns double precision;
create FUNCTION float8_regr_sxx($1 double precision[]) returns double precision;
create FUNCTION float8_regr_sxy($1 double precision[]) returns double precision;
create FUNCTION float8_regr_syy($1 double precision[]) returns double precision;
create FUNCTION float8_stddev_pop($1 double precision[]) returns double precision;
create FUNCTION float8_stddev_samp($1 double precision[]) returns double precision;
create FUNCTION float8_var_pop($1 double precision[]) returns double precision;
create FUNCTION float8_var_samp($1 double precision[]) returns double precision;
create FUNCTION float8abs($1 double precision) returns double precision;
create FUNCTION float8($1 bigint) returns double precision;
create FUNCTION float8div($1 double precision, $2 double precision) returns double precision;
create FUNCTION float8eq($1 double precision, $2 double precision) returns boolean;
create FUNCTION float8ge($1 double precision, $2 double precision) returns boolean;
create FUNCTION float8gt($1 double precision, $2 double precision) returns boolean;
create FUNCTION float8in($1 cstring) returns double precision;
create FUNCTION float8($1 integer) returns double precision;
create FUNCTION float8larger($1 double precision, $2 double precision) returns double precision;
create FUNCTION float8le($1 double precision, $2 double precision) returns boolean;
create FUNCTION float8lt($1 double precision, $2 double precision) returns boolean;
create FUNCTION float8mi($1 double precision, $2 double precision) returns double precision;
create FUNCTION float8mul($1 double precision, $2 double precision) returns double precision;
create FUNCTION float8ne($1 double precision, $2 double precision) returns boolean;
create FUNCTION float8($1 numeric) returns double precision;
create FUNCTION float8out($1 double precision) returns cstring;
create FUNCTION float8pl($1 double precision, $2 double precision) returns double precision;
create FUNCTION float8($1 real) returns double precision;
create FUNCTION float8recv($1 internal) returns double precision;
create FUNCTION float8send($1 double precision) returns bytea;
create FUNCTION float8smaller($1 double precision, $2 double precision) returns double precision;
create FUNCTION float8($1 smallint) returns double precision;
create FUNCTION float8um($1 double precision) returns double precision;
create FUNCTION float8up($1 double precision) returns double precision;
create FUNCTION floor($1 double precision) returns double precision;
create FUNCTION floor($1 numeric) returns numeric;
create FUNCTION flt4_mul_cash($1 real, $2 money) returns money;
create FUNCTION flt8_mul_cash($1 double precision, $2 money) returns money;
create FUNCTION fmgr_c_validator($1 oid) returns void;
create FUNCTION fmgr_internal_validator($1 oid) returns void;
create FUNCTION fmgr_sql_validator($1 oid) returns void;
create FUNCTION format_type($1 oid, $2 integer) returns text;
create FUNCTION format($1 text) returns text;
create FUNCTION format($1 text, $2 "any") returns text;
create FUNCTION gb18030_to_utf8($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION gbk_to_utf8($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION generate_series($1 bigint, $2 bigint) returns setof bigint;
create FUNCTION generate_series($1 bigint, $2 bigint, $3 bigint) returns setof bigint;
create FUNCTION generate_series($1 integer, $2 integer) returns setof integer;
create FUNCTION generate_series($1 integer, $2 integer, $3 integer) returns setof integer;
create FUNCTION generate_series($1 timestamp with time zone, $2 timestamp with time zone, $3 interval) returns setof timestamp with time zone;
create FUNCTION generate_series($1 timestamp, $2 timestamp, $3 interval) returns setof timestamp;
create FUNCTION generate_subscripts($1 anyarray, $2 integer) returns setof integer;
create FUNCTION generate_subscripts($1 anyarray, $2 integer, $3 boolean) returns setof integer;
create FUNCTION get_bit($1 bit, $2 integer) returns integer;
create FUNCTION get_bit($1 bytea, $2 integer) returns integer;
create FUNCTION get_byte($1 bytea, $2 integer) returns integer;
create FUNCTION get_current_ts_config();
create FUNCTION getdatabaseencoding();
create FUNCTION getpgusername();
create FUNCTION gin_cmp_prefix($1 text, $2 text, $3 smallint, $4 internal) returns integer;
create FUNCTION gin_cmp_tslexeme($1 text, $2 text) returns integer;
create FUNCTION gin_compare_jsonb($1 text, $2 text) returns integer;
create FUNCTION gin_consistent_jsonb_path($1 internal, $2 smallint, $3 anyarray, $4 integer, $5 internal, $6 internal, $7 internal, $8 internal) returns boolean;
create FUNCTION gin_consistent_jsonb($1 internal, $2 smallint, $3 anyarray, $4 integer, $5 internal, $6 internal, $7 internal, $8 internal) returns boolean;
create FUNCTION gin_extract_jsonb_path($1 internal, $2 internal, $3 internal) returns internal;
create FUNCTION gin_extract_jsonb_query_path($1 anyarray, $2 internal, $3 smallint, $4 internal, $5 internal, $6 internal, $7 internal) returns internal;
create FUNCTION gin_extract_jsonb_query($1 anyarray, $2 internal, $3 smallint, $4 internal, $5 internal, $6 internal, $7 internal) returns internal;
create FUNCTION gin_extract_jsonb($1 internal, $2 internal, $3 internal) returns internal;
create FUNCTION gin_extract_tsquery($1 tsquery, $2 internal, $3 smallint, $4 internal, $5 internal) returns internal;
create FUNCTION gin_extract_tsquery($1 tsquery, $2 internal, $3 smallint, $4 internal, $5 internal, $6 internal, $7 internal) returns internal;
create FUNCTION gin_extract_tsvector($1 tsvector, $2 internal) returns internal;
create FUNCTION gin_extract_tsvector($1 tsvector, $2 internal, $3 internal) returns internal;
create FUNCTION gin_triconsistent_jsonb_path($1 internal, $2 smallint, $3 anyarray, $4 integer, $5 internal, $6 internal, $7 internal) returns "char";
create FUNCTION gin_triconsistent_jsonb($1 internal, $2 smallint, $3 anyarray, $4 integer, $5 internal, $6 internal, $7 internal) returns "char";
create FUNCTION gin_tsquery_consistent($1 internal, $2 smallint, $3 tsquery, $4 integer, $5 internal, $6 internal) returns boolean;
create FUNCTION gin_tsquery_consistent($1 internal, $2 smallint, $3 tsquery, $4 integer, $5 internal, $6 internal, $7 internal, $8 internal) returns boolean;
create FUNCTION gin_tsquery_triconsistent($1 internal, $2 smallint, $3 tsquery, $4 integer, $5 internal, $6 internal, $7 internal) returns "char";
create FUNCTION ginarrayconsistent($1 internal, $2 smallint, $3 anyarray, $4 integer, $5 internal, $6 internal, $7 internal, $8 internal) returns boolean;
create FUNCTION ginarrayextract($1 anyarray, $2 internal) returns internal;
create FUNCTION ginarrayextract($1 anyarray, $2 internal, $3 internal) returns internal;
create FUNCTION ginarraytriconsistent($1 internal, $2 smallint, $3 anyarray, $4 integer, $5 internal, $6 internal, $7 internal) returns "char";
create FUNCTION ginbeginscan($1 internal, $2 internal, $3 internal) returns internal;
create FUNCTION ginbuildempty($1 internal) returns void;
create FUNCTION ginbuild($1 internal, $2 internal, $3 internal) returns internal;
create FUNCTION ginbulkdelete($1 internal, $2 internal, $3 internal, $4 internal) returns internal;
create FUNCTION gincostestimate($1 internal, $2 internal, $3 internal, $4 internal, $5 internal, $6 internal, $7 internal) returns void;
create FUNCTION ginendscan($1 internal) returns void;
create FUNCTION gingetbitmap($1 internal, $2 internal) returns bigint;
create FUNCTION gininsert($1 internal, $2 internal, $3 internal, $4 internal, $5 internal, $6 internal) returns boolean;
create FUNCTION ginmarkpos($1 internal) returns void;
create FUNCTION ginoptions($1 text[], $2 boolean) returns bytea;
create FUNCTION ginqueryarrayextract($1 anyarray, $2 internal, $3 smallint, $4 internal, $5 internal, $6 internal, $7 internal) returns internal;
create FUNCTION ginrescan($1 internal, $2 internal, $3 internal, $4 internal, $5 internal) returns void;
create FUNCTION ginrestrpos($1 internal) returns void;
create FUNCTION ginvacuumcleanup($1 internal, $2 internal) returns internal;
create FUNCTION gist_box_compress($1 internal) returns internal;
create FUNCTION gist_box_consistent($1 internal, $2 box, $3 integer, $4 oid, $5 internal) returns boolean;
create FUNCTION gist_box_decompress($1 internal) returns internal;
create FUNCTION gist_box_penalty($1 internal, $2 internal, $3 internal) returns internal;
create FUNCTION gist_box_picksplit($1 internal, $2 internal) returns internal;
create FUNCTION gist_box_same($1 box, $2 box, $3 internal) returns internal;
create FUNCTION gist_box_union($1 internal, $2 internal) returns box;
create FUNCTION gist_circle_compress($1 internal) returns internal;
create FUNCTION gist_circle_consistent($1 internal, $2 circle, $3 integer, $4 oid, $5 internal) returns boolean;
create FUNCTION gist_point_compress($1 internal) returns internal;
create FUNCTION gist_point_consistent($1 internal, $2 point, $3 integer, $4 oid, $5 internal) returns boolean;
create FUNCTION gist_point_distance($1 internal, $2 point, $3 integer, $4 oid) returns double precision;
create FUNCTION gist_poly_compress($1 internal) returns internal;
create FUNCTION gist_poly_consistent($1 internal, $2 polygon, $3 integer, $4 oid, $5 internal) returns boolean;
create FUNCTION gistbeginscan($1 internal, $2 internal, $3 internal) returns internal;
create FUNCTION gistbuildempty($1 internal) returns void;
create FUNCTION gistbuild($1 internal, $2 internal, $3 internal) returns internal;
create FUNCTION gistbulkdelete($1 internal, $2 internal, $3 internal, $4 internal) returns internal;
create FUNCTION gistcostestimate($1 internal, $2 internal, $3 internal, $4 internal, $5 internal, $6 internal, $7 internal) returns void;
create FUNCTION gistendscan($1 internal) returns void;
create FUNCTION gistgetbitmap($1 internal, $2 internal) returns bigint;
create FUNCTION gistgettuple($1 internal, $2 internal) returns boolean;
create FUNCTION gistinsert($1 internal, $2 internal, $3 internal, $4 internal, $5 internal, $6 internal) returns boolean;
create FUNCTION gistmarkpos($1 internal) returns void;
create FUNCTION gistoptions($1 text[], $2 boolean) returns bytea;
create FUNCTION gistrescan($1 internal, $2 internal, $3 internal, $4 internal, $5 internal) returns void;
create FUNCTION gistrestrpos($1 internal) returns void;
create FUNCTION gistvacuumcleanup($1 internal, $2 internal) returns internal;
create FUNCTION gtsquery_compress($1 internal) returns internal;
create FUNCTION gtsquery_consistent($1 internal, $2 internal, $3 integer, $4 oid, $5 internal) returns boolean;
create FUNCTION gtsquery_decompress($1 internal) returns internal;
create FUNCTION gtsquery_penalty($1 internal, $2 internal, $3 internal) returns internal;
create FUNCTION gtsquery_picksplit($1 internal, $2 internal) returns internal;
create FUNCTION gtsquery_same($1 bigint, $2 bigint, $3 internal) returns internal;
create FUNCTION gtsquery_union($1 internal, $2 internal) returns internal;
create FUNCTION gtsvector_compress($1 internal) returns internal;
create FUNCTION gtsvector_consistent($1 internal, $2 gtsvector, $3 integer, $4 oid, $5 internal) returns boolean;
create FUNCTION gtsvector_decompress($1 internal) returns internal;
create FUNCTION gtsvector_penalty($1 internal, $2 internal, $3 internal) returns internal;
create FUNCTION gtsvector_picksplit($1 internal, $2 internal) returns internal;
create FUNCTION gtsvector_same($1 gtsvector, $2 gtsvector, $3 internal) returns internal;
create FUNCTION gtsvector_union($1 internal, $2 internal) returns internal;
create FUNCTION gtsvectorin($1 cstring) returns gtsvector;
create FUNCTION gtsvectorout($1 gtsvector) returns cstring;
create FUNCTION has_any_column_privilege($1 name, $2 oid, $3 text) returns boolean;
create FUNCTION has_any_column_privilege($1 name, $2 text, $3 text) returns boolean;
create FUNCTION has_any_column_privilege($1 oid, $2 oid, $3 text) returns boolean;
create FUNCTION has_any_column_privilege($1 oid, $2 text) returns boolean;
create FUNCTION has_any_column_privilege($1 oid, $2 text, $3 text) returns boolean;
create FUNCTION has_any_column_privilege($1 text, $2 text) returns boolean;
create FUNCTION has_column_privilege($1 name, $2 oid, $3 smallint, $4 text) returns boolean;
create FUNCTION has_column_privilege($1 name, $2 oid, $3 text, $4 text) returns boolean;
create FUNCTION has_column_privilege($1 name, $2 text, $3 smallint, $4 text) returns boolean;
create FUNCTION has_column_privilege($1 name, $2 text, $3 text, $4 text) returns boolean;
create FUNCTION has_column_privilege($1 oid, $2 oid, $3 smallint, $4 text) returns boolean;
create FUNCTION has_column_privilege($1 oid, $2 oid, $3 text, $4 text) returns boolean;
create FUNCTION has_column_privilege($1 oid, $2 smallint, $3 text) returns boolean;
create FUNCTION has_column_privilege($1 oid, $2 text, $3 smallint, $4 text) returns boolean;
create FUNCTION has_column_privilege($1 oid, $2 text, $3 text) returns boolean;
create FUNCTION has_column_privilege($1 oid, $2 text, $3 text, $4 text) returns boolean;
create FUNCTION has_column_privilege($1 text, $2 smallint, $3 text) returns boolean;
create FUNCTION has_column_privilege($1 text, $2 text, $3 text) returns boolean;
create FUNCTION has_database_privilege($1 name, $2 oid, $3 text) returns boolean;
create FUNCTION has_database_privilege($1 name, $2 text, $3 text) returns boolean;
create FUNCTION has_database_privilege($1 oid, $2 oid, $3 text) returns boolean;
create FUNCTION has_database_privilege($1 oid, $2 text) returns boolean;
create FUNCTION has_database_privilege($1 oid, $2 text, $3 text) returns boolean;
create FUNCTION has_database_privilege($1 text, $2 text) returns boolean;
create FUNCTION has_foreign_data_wrapper_privilege($1 name, $2 oid, $3 text) returns boolean;
create FUNCTION has_foreign_data_wrapper_privilege($1 name, $2 text, $3 text) returns boolean;
create FUNCTION has_foreign_data_wrapper_privilege($1 oid, $2 oid, $3 text) returns boolean;
create FUNCTION has_foreign_data_wrapper_privilege($1 oid, $2 text) returns boolean;
create FUNCTION has_foreign_data_wrapper_privilege($1 oid, $2 text, $3 text) returns boolean;
create FUNCTION has_foreign_data_wrapper_privilege($1 text, $2 text) returns boolean;
create FUNCTION has_function_privilege($1 name, $2 oid, $3 text) returns boolean;
create FUNCTION has_function_privilege($1 name, $2 text, $3 text) returns boolean;
create FUNCTION has_function_privilege($1 oid, $2 oid, $3 text) returns boolean;
create FUNCTION has_function_privilege($1 oid, $2 text) returns boolean;
create FUNCTION has_function_privilege($1 oid, $2 text, $3 text) returns boolean;
create FUNCTION has_function_privilege($1 text, $2 text) returns boolean;
create FUNCTION has_language_privilege($1 name, $2 oid, $3 text) returns boolean;
create FUNCTION has_language_privilege($1 name, $2 text, $3 text) returns boolean;
create FUNCTION has_language_privilege($1 oid, $2 oid, $3 text) returns boolean;
create FUNCTION has_language_privilege($1 oid, $2 text) returns boolean;
create FUNCTION has_language_privilege($1 oid, $2 text, $3 text) returns boolean;
create FUNCTION has_language_privilege($1 text, $2 text) returns boolean;
create FUNCTION has_schema_privilege($1 name, $2 oid, $3 text) returns boolean;
create FUNCTION has_schema_privilege($1 name, $2 text, $3 text) returns boolean;
create FUNCTION has_schema_privilege($1 oid, $2 oid, $3 text) returns boolean;
create FUNCTION has_schema_privilege($1 oid, $2 text) returns boolean;
create FUNCTION has_schema_privilege($1 oid, $2 text, $3 text) returns boolean;
create FUNCTION has_schema_privilege($1 text, $2 text) returns boolean;
create FUNCTION has_sequence_privilege($1 name, $2 oid, $3 text) returns boolean;
create FUNCTION has_sequence_privilege($1 name, $2 text, $3 text) returns boolean;
create FUNCTION has_sequence_privilege($1 oid, $2 oid, $3 text) returns boolean;
create FUNCTION has_sequence_privilege($1 oid, $2 text) returns boolean;
create FUNCTION has_sequence_privilege($1 oid, $2 text, $3 text) returns boolean;
create FUNCTION has_sequence_privilege($1 text, $2 text) returns boolean;
create FUNCTION has_server_privilege($1 name, $2 oid, $3 text) returns boolean;
create FUNCTION has_server_privilege($1 name, $2 text, $3 text) returns boolean;
create FUNCTION has_server_privilege($1 oid, $2 oid, $3 text) returns boolean;
create FUNCTION has_server_privilege($1 oid, $2 text) returns boolean;
create FUNCTION has_server_privilege($1 oid, $2 text, $3 text) returns boolean;
create FUNCTION has_server_privilege($1 text, $2 text) returns boolean;
create FUNCTION has_table_privilege($1 name, $2 oid, $3 text) returns boolean;
create FUNCTION has_table_privilege($1 name, $2 text, $3 text) returns boolean;
create FUNCTION has_table_privilege($1 oid, $2 oid, $3 text) returns boolean;
create FUNCTION has_table_privilege($1 oid, $2 text) returns boolean;
create FUNCTION has_table_privilege($1 oid, $2 text, $3 text) returns boolean;
create FUNCTION has_table_privilege($1 text, $2 text) returns boolean;
create FUNCTION has_tablespace_privilege($1 name, $2 oid, $3 text) returns boolean;
create FUNCTION has_tablespace_privilege($1 name, $2 text, $3 text) returns boolean;
create FUNCTION has_tablespace_privilege($1 oid, $2 oid, $3 text) returns boolean;
create FUNCTION has_tablespace_privilege($1 oid, $2 text) returns boolean;
create FUNCTION has_tablespace_privilege($1 oid, $2 text, $3 text) returns boolean;
create FUNCTION has_tablespace_privilege($1 text, $2 text) returns boolean;
create FUNCTION has_type_privilege($1 name, $2 oid, $3 text) returns boolean;
create FUNCTION has_type_privilege($1 name, $2 text, $3 text) returns boolean;
create FUNCTION has_type_privilege($1 oid, $2 oid, $3 text) returns boolean;
create FUNCTION has_type_privilege($1 oid, $2 text) returns boolean;
create FUNCTION has_type_privilege($1 oid, $2 text, $3 text) returns boolean;
create FUNCTION has_type_privilege($1 text, $2 text) returns boolean;
create FUNCTION hash_aclitem($1 aclitem) returns integer;
create FUNCTION hash_array($1 anyarray) returns integer;
create FUNCTION hash_numeric($1 numeric) returns integer;
create FUNCTION hash_range($1 anyrange) returns integer;
create FUNCTION hashbeginscan($1 internal, $2 internal, $3 internal) returns internal;
create FUNCTION hashbpchar($1 char) returns integer;
create FUNCTION hashbuildempty($1 internal) returns void;
create FUNCTION hashbuild($1 internal, $2 internal, $3 internal) returns internal;
create FUNCTION hashbulkdelete($1 internal, $2 internal, $3 internal, $4 internal) returns internal;
create FUNCTION hashchar($1 "char") returns integer;
create FUNCTION hashcostestimate($1 internal, $2 internal, $3 internal, $4 internal, $5 internal, $6 internal, $7 internal) returns void;
create FUNCTION hashendscan($1 internal) returns void;
create FUNCTION hashenum($1 anyenum) returns integer;
create FUNCTION hashfloat4($1 real) returns integer;
create FUNCTION hashfloat8($1 double precision) returns integer;
create FUNCTION hashgetbitmap($1 internal, $2 internal) returns bigint;
create FUNCTION hashgettuple($1 internal, $2 internal) returns boolean;
create FUNCTION hashinet($1 inet) returns integer;
create FUNCTION hashinsert($1 internal, $2 internal, $3 internal, $4 internal, $5 internal, $6 internal) returns boolean;
create FUNCTION hashint2($1 smallint) returns integer;
create FUNCTION hashint2vector($1 int2vector) returns integer;
create FUNCTION hashint4($1 integer) returns integer;
create FUNCTION hashint8($1 bigint) returns integer;
create FUNCTION hashmacaddr($1 macaddr) returns integer;
create FUNCTION hashmarkpos($1 internal) returns void;
create FUNCTION hashname($1 name) returns integer;
create FUNCTION hashoid($1 oid) returns integer;
create FUNCTION hashoidvector($1 oidvector) returns integer;
create FUNCTION hashoptions($1 text[], $2 boolean) returns bytea;
create FUNCTION hashrescan($1 internal, $2 internal, $3 internal, $4 internal, $5 internal) returns void;
create FUNCTION hashrestrpos($1 internal) returns void;
create FUNCTION hashtext($1 text) returns integer;
create FUNCTION hashvacuumcleanup($1 internal, $2 internal) returns internal;
create FUNCTION hashvarlena($1 internal) returns integer;
create FUNCTION height($1 box) returns double precision;
create FUNCTION host($1 inet) returns text;
create FUNCTION hostmask($1 inet) returns inet;
create FUNCTION iclikejoinsel($1 internal, $2 oid, $3 internal, $4 smallint, $5 internal) returns double precision;
create FUNCTION iclikesel($1 internal, $2 oid, $3 internal, $4 integer) returns double precision;
create FUNCTION icnlikejoinsel($1 internal, $2 oid, $3 internal, $4 smallint, $5 internal) returns double precision;
create FUNCTION icnlikesel($1 internal, $2 oid, $3 internal, $4 integer) returns double precision;
create FUNCTION icregexeqjoinsel($1 internal, $2 oid, $3 internal, $4 smallint, $5 internal) returns double precision;
create FUNCTION icregexeqsel($1 internal, $2 oid, $3 internal, $4 integer) returns double precision;
create FUNCTION icregexnejoinsel($1 internal, $2 oid, $3 internal, $4 smallint, $5 internal) returns double precision;
create FUNCTION icregexnesel($1 internal, $2 oid, $3 internal, $4 integer) returns double precision;
create FUNCTION inet_client_addr();
create FUNCTION inet_client_port();
create FUNCTION inet_gist_compress($1 internal) returns internal;
create FUNCTION inet_gist_consistent($1 internal, $2 inet, $3 integer, $4 oid, $5 internal) returns boolean;
create FUNCTION inet_gist_decompress($1 internal) returns internal;
create FUNCTION inet_gist_penalty($1 internal, $2 internal, $3 internal) returns internal;
create FUNCTION inet_gist_picksplit($1 internal, $2 internal) returns internal;
create FUNCTION inet_gist_same($1 inet, $2 inet, $3 internal) returns internal;
create FUNCTION inet_gist_union($1 internal, $2 internal) returns internal;
create FUNCTION inet_in($1 cstring) returns inet;
create FUNCTION inet_out($1 inet) returns cstring;
create FUNCTION inet_recv($1 internal) returns inet;
create FUNCTION inet_send($1 inet) returns bytea;
create FUNCTION inet_server_addr();
create FUNCTION inet_server_port();
create FUNCTION inetand($1 inet, $2 inet) returns inet;
create FUNCTION inetmi_int8($1 inet, $2 bigint) returns inet;
create FUNCTION inetmi($1 inet, $2 inet) returns bigint;
create FUNCTION inetnot($1 inet) returns inet;
create FUNCTION inetor($1 inet, $2 inet) returns inet;
create FUNCTION inetpl($1 inet, $2 bigint) returns inet;
create FUNCTION initcap($1 text) returns text;
create FUNCTION int24div($1 smallint, $2 integer) returns integer;
create FUNCTION int24eq($1 smallint, $2 integer) returns boolean;
create FUNCTION int24ge($1 smallint, $2 integer) returns boolean;
create FUNCTION int24gt($1 smallint, $2 integer) returns boolean;
create FUNCTION int24le($1 smallint, $2 integer) returns boolean;
create FUNCTION int24lt($1 smallint, $2 integer) returns boolean;
create FUNCTION int24mi($1 smallint, $2 integer) returns integer;
create FUNCTION int24mul($1 smallint, $2 integer) returns integer;
create FUNCTION int24ne($1 smallint, $2 integer) returns boolean;
create FUNCTION int24pl($1 smallint, $2 integer) returns integer;
create FUNCTION int28div($1 smallint, $2 bigint) returns bigint;
create FUNCTION int28eq($1 smallint, $2 bigint) returns boolean;
create FUNCTION int28ge($1 smallint, $2 bigint) returns boolean;
create FUNCTION int28gt($1 smallint, $2 bigint) returns boolean;
create FUNCTION int28le($1 smallint, $2 bigint) returns boolean;
create FUNCTION int28lt($1 smallint, $2 bigint) returns boolean;
create FUNCTION int28mi($1 smallint, $2 bigint) returns bigint;
create FUNCTION int28mul($1 smallint, $2 bigint) returns bigint;
create FUNCTION int28ne($1 smallint, $2 bigint) returns boolean;
create FUNCTION int28pl($1 smallint, $2 bigint) returns bigint;
create FUNCTION int2_accum_inv($1 internal, $2 smallint) returns internal;
create FUNCTION int2_accum($1 internal, $2 smallint) returns internal;
create FUNCTION int2_avg_accum_inv($1 bigint[], $2 smallint) returns bigint[];
create FUNCTION int2_avg_accum($1 bigint[], $2 smallint) returns bigint[];
create FUNCTION int2_mul_cash($1 smallint, $2 money) returns money;
create FUNCTION int2_sum($1 bigint, $2 smallint) returns bigint;
create FUNCTION int2abs($1 smallint) returns smallint;
create FUNCTION int2and($1 smallint, $2 smallint) returns smallint;
create FUNCTION int2($1 bigint) returns smallint;
create FUNCTION int2div($1 smallint, $2 smallint) returns smallint;
create FUNCTION int2($1 double precision) returns smallint;
create FUNCTION int2eq($1 smallint, $2 smallint) returns boolean;
create FUNCTION int2ge($1 smallint, $2 smallint) returns boolean;
create FUNCTION int2gt($1 smallint, $2 smallint) returns boolean;
create FUNCTION int2in($1 cstring) returns smallint;
create FUNCTION int2int4_sum($1 bigint[]) returns bigint;
create FUNCTION int2($1 integer) returns smallint;
create FUNCTION int2larger($1 smallint, $2 smallint) returns smallint;
create FUNCTION int2le($1 smallint, $2 smallint) returns boolean;
create FUNCTION int2lt($1 smallint, $2 smallint) returns boolean;
create FUNCTION int2mi($1 smallint, $2 smallint) returns smallint;
create FUNCTION int2mod($1 smallint, $2 smallint) returns smallint;
create FUNCTION int2mul($1 smallint, $2 smallint) returns smallint;
create FUNCTION int2ne($1 smallint, $2 smallint) returns boolean;
create FUNCTION int2not($1 smallint) returns smallint;
create FUNCTION int2($1 numeric) returns smallint;
create FUNCTION int2or($1 smallint, $2 smallint) returns smallint;
create FUNCTION int2out($1 smallint) returns cstring;
create FUNCTION int2pl($1 smallint, $2 smallint) returns smallint;
create FUNCTION int2($1 real) returns smallint;
create FUNCTION int2recv($1 internal) returns smallint;
create FUNCTION int2send($1 smallint) returns bytea;
create FUNCTION int2shl($1 smallint, $2 integer) returns smallint;
create FUNCTION int2shr($1 smallint, $2 integer) returns smallint;
create FUNCTION int2smaller($1 smallint, $2 smallint) returns smallint;
create FUNCTION int2um($1 smallint) returns smallint;
create FUNCTION int2up($1 smallint) returns smallint;
create FUNCTION int2vectoreq($1 int2vector, $2 int2vector) returns boolean;
create FUNCTION int2vectorin($1 cstring) returns int2vector;
create FUNCTION int2vectorout($1 int2vector) returns cstring;
create FUNCTION int2vectorrecv($1 internal) returns int2vector;
create FUNCTION int2vectorsend($1 int2vector) returns bytea;
create FUNCTION int2xor($1 smallint, $2 smallint) returns smallint;
create FUNCTION int4($1 "char") returns integer;
create FUNCTION int42div($1 integer, $2 smallint) returns integer;
create FUNCTION int42eq($1 integer, $2 smallint) returns boolean;
create FUNCTION int42ge($1 integer, $2 smallint) returns boolean;
create FUNCTION int42gt($1 integer, $2 smallint) returns boolean;
create FUNCTION int42le($1 integer, $2 smallint) returns boolean;
create FUNCTION int42lt($1 integer, $2 smallint) returns boolean;
create FUNCTION int42mi($1 integer, $2 smallint) returns integer;
create FUNCTION int42mul($1 integer, $2 smallint) returns integer;
create FUNCTION int42ne($1 integer, $2 smallint) returns boolean;
create FUNCTION int42pl($1 integer, $2 smallint) returns integer;
create FUNCTION int48div($1 integer, $2 bigint) returns bigint;
create FUNCTION int48eq($1 integer, $2 bigint) returns boolean;
create FUNCTION int48ge($1 integer, $2 bigint) returns boolean;
create FUNCTION int48gt($1 integer, $2 bigint) returns boolean;
create FUNCTION int48le($1 integer, $2 bigint) returns boolean;
create FUNCTION int48lt($1 integer, $2 bigint) returns boolean;
create FUNCTION int48mi($1 integer, $2 bigint) returns bigint;
create FUNCTION int48mul($1 integer, $2 bigint) returns bigint;
create FUNCTION int48ne($1 integer, $2 bigint) returns boolean;
create FUNCTION int48pl($1 integer, $2 bigint) returns bigint;
create FUNCTION int4_accum_inv($1 internal, $2 integer) returns internal;
create FUNCTION int4_accum($1 internal, $2 integer) returns internal;
create FUNCTION int4_avg_accum_inv($1 bigint[], $2 integer) returns bigint[];
create FUNCTION int4_avg_accum($1 bigint[], $2 integer) returns bigint[];
create FUNCTION int4_mul_cash($1 integer, $2 money) returns money;
create FUNCTION int4_sum($1 bigint, $2 integer) returns bigint;
create FUNCTION int4abs($1 integer) returns integer;
create FUNCTION int4and($1 integer, $2 integer) returns integer;
create FUNCTION int4($1 bigint) returns integer;
create FUNCTION int4($1 bit) returns integer;
create FUNCTION int4($1 boolean) returns integer;
create FUNCTION int4div($1 integer, $2 integer) returns integer;
create FUNCTION int4($1 double precision) returns integer;
create FUNCTION int4eq($1 integer, $2 integer) returns boolean;
create FUNCTION int4ge($1 integer, $2 integer) returns boolean;
create FUNCTION int4gt($1 integer, $2 integer) returns boolean;
create FUNCTION int4inc($1 integer) returns integer;
create FUNCTION int4in($1 cstring) returns integer;
create FUNCTION int4larger($1 integer, $2 integer) returns integer;
create FUNCTION int4le($1 integer, $2 integer) returns boolean;
create FUNCTION int4lt($1 integer, $2 integer) returns boolean;
create FUNCTION int4mi($1 integer, $2 integer) returns integer;
create FUNCTION int4mod($1 integer, $2 integer) returns integer;
create FUNCTION int4mul($1 integer, $2 integer) returns integer;
create FUNCTION int4ne($1 integer, $2 integer) returns boolean;
create FUNCTION int4not($1 integer) returns integer;
create FUNCTION int4($1 numeric) returns integer;
create FUNCTION int4or($1 integer, $2 integer) returns integer;
create FUNCTION int4out($1 integer) returns cstring;
create FUNCTION int4pl($1 integer, $2 integer) returns integer;
create FUNCTION int4range_canonical($1 int4range) returns int4range;
create FUNCTION int4range_subdiff($1 integer, $2 integer) returns double precision;
create FUNCTION int4range($1 integer, $2 integer) returns int4range;
create FUNCTION int4range($1 integer, $2 integer, $3 text) returns int4range;
create FUNCTION int4($1 real) returns integer;
create FUNCTION int4recv($1 internal) returns integer;
create FUNCTION int4send($1 integer) returns bytea;
create FUNCTION int4shl($1 integer, $2 integer) returns integer;
create FUNCTION int4shr($1 integer, $2 integer) returns integer;
create FUNCTION int4smaller($1 integer, $2 integer) returns integer;
create FUNCTION int4($1 smallint) returns integer;
create FUNCTION int4um($1 integer) returns integer;
create FUNCTION int4up($1 integer) returns integer;
create FUNCTION int4xor($1 integer, $2 integer) returns integer;
create FUNCTION int82div($1 bigint, $2 smallint) returns bigint;
create FUNCTION int82eq($1 bigint, $2 smallint) returns boolean;
create FUNCTION int82ge($1 bigint, $2 smallint) returns boolean;
create FUNCTION int82gt($1 bigint, $2 smallint) returns boolean;
create FUNCTION int82le($1 bigint, $2 smallint) returns boolean;
create FUNCTION int82lt($1 bigint, $2 smallint) returns boolean;
create FUNCTION int82mi($1 bigint, $2 smallint) returns bigint;
create FUNCTION int82mul($1 bigint, $2 smallint) returns bigint;
create FUNCTION int82ne($1 bigint, $2 smallint) returns boolean;
create FUNCTION int82pl($1 bigint, $2 smallint) returns bigint;
create FUNCTION int84div($1 bigint, $2 integer) returns bigint;
create FUNCTION int84eq($1 bigint, $2 integer) returns boolean;
create FUNCTION int84ge($1 bigint, $2 integer) returns boolean;
create FUNCTION int84gt($1 bigint, $2 integer) returns boolean;
create FUNCTION int84le($1 bigint, $2 integer) returns boolean;
create FUNCTION int84lt($1 bigint, $2 integer) returns boolean;
create FUNCTION int84mi($1 bigint, $2 integer) returns bigint;
create FUNCTION int84mul($1 bigint, $2 integer) returns bigint;
create FUNCTION int84ne($1 bigint, $2 integer) returns boolean;
create FUNCTION int84pl($1 bigint, $2 integer) returns bigint;
create FUNCTION int8_accum_inv($1 internal, $2 bigint) returns internal;
create FUNCTION int8_accum($1 internal, $2 bigint) returns internal;
create FUNCTION int8_avg_accum($1 internal, $2 bigint) returns internal;
create FUNCTION int8_avg($1 bigint[]) returns numeric;
create FUNCTION int8_sum($1 numeric, $2 bigint) returns numeric;
create FUNCTION int8abs($1 bigint) returns bigint;
create FUNCTION int8and($1 bigint, $2 bigint) returns bigint;
create FUNCTION int8($1 bit) returns bigint;
create FUNCTION int8dec_any($1 bigint, $2 "any") returns bigint;
create FUNCTION int8dec($1 bigint) returns bigint;
create FUNCTION int8div($1 bigint, $2 bigint) returns bigint;
create FUNCTION int8($1 double precision) returns bigint;
create FUNCTION int8eq($1 bigint, $2 bigint) returns boolean;
create FUNCTION int8ge($1 bigint, $2 bigint) returns boolean;
create FUNCTION int8gt($1 bigint, $2 bigint) returns boolean;
create FUNCTION int8inc_any($1 bigint, $2 "any") returns bigint;
create FUNCTION int8inc_float8_float8($1 bigint, $2 double precision, $3 double precision) returns bigint;
create FUNCTION int8inc($1 bigint) returns bigint;
create FUNCTION int8in($1 cstring) returns bigint;
create FUNCTION int8($1 integer) returns bigint;
create FUNCTION int8larger($1 bigint, $2 bigint) returns bigint;
create FUNCTION int8le($1 bigint, $2 bigint) returns boolean;
create FUNCTION int8lt($1 bigint, $2 bigint) returns boolean;
create FUNCTION int8mi($1 bigint, $2 bigint) returns bigint;
create FUNCTION int8mod($1 bigint, $2 bigint) returns bigint;
create FUNCTION int8mul($1 bigint, $2 bigint) returns bigint;
create FUNCTION int8ne($1 bigint, $2 bigint) returns boolean;
create FUNCTION int8not($1 bigint) returns bigint;
create FUNCTION int8($1 numeric) returns bigint;
create FUNCTION int8($1 oid) returns bigint;
create FUNCTION int8or($1 bigint, $2 bigint) returns bigint;
create FUNCTION int8out($1 bigint) returns cstring;
create FUNCTION int8pl_inet($1 bigint, $2 inet) returns inet;
create FUNCTION int8pl($1 bigint, $2 bigint) returns bigint;
create FUNCTION int8range_canonical($1 int8range) returns int8range;
create FUNCTION int8range_subdiff($1 bigint, $2 bigint) returns double precision;
create FUNCTION int8range($1 bigint, $2 bigint) returns int8range;
create FUNCTION int8range($1 bigint, $2 bigint, $3 text) returns int8range;
create FUNCTION int8($1 real) returns bigint;
create FUNCTION int8recv($1 internal) returns bigint;
create FUNCTION int8send($1 bigint) returns bytea;
create FUNCTION int8shl($1 bigint, $2 integer) returns bigint;
create FUNCTION int8shr($1 bigint, $2 integer) returns bigint;
create FUNCTION int8smaller($1 bigint, $2 bigint) returns bigint;
create FUNCTION int8($1 smallint) returns bigint;
create FUNCTION int8um($1 bigint) returns bigint;
create FUNCTION int8up($1 bigint) returns bigint;
create FUNCTION int8xor($1 bigint, $2 bigint) returns bigint;
create FUNCTION integer_pl_date($1 integer, $2 date) returns date;
create FUNCTION inter_lb($1 line, $2 box) returns boolean;
create FUNCTION inter_sb($1 lseg, $2 box) returns boolean;
create FUNCTION inter_sl($1 lseg, $2 line) returns boolean;
create FUNCTION internal_in($1 cstring) returns internal;
create FUNCTION internal_out($1 internal) returns cstring;
create FUNCTION interval_accum_inv($1 interval[], $2 interval) returns interval[];
create FUNCTION interval_accum($1 interval[], $2 interval) returns interval[];
create FUNCTION interval_avg($1 interval[]) returns interval;
create FUNCTION interval_cmp($1 interval, $2 interval) returns integer;
create FUNCTION interval_div($1 interval, $2 double precision) returns interval;
create FUNCTION interval_eq($1 interval, $2 interval) returns boolean;
create FUNCTION interval_ge($1 interval, $2 interval) returns boolean;
create FUNCTION interval_gt($1 interval, $2 interval) returns boolean;
create FUNCTION interval_hash($1 interval) returns integer;
create FUNCTION interval_in($1 cstring, $2 oid, $3 integer) returns interval;
create FUNCTION interval_larger($1 interval, $2 interval) returns interval;
create FUNCTION interval_le($1 interval, $2 interval) returns boolean;
create FUNCTION interval_lt($1 interval, $2 interval) returns boolean;
create FUNCTION interval_mi($1 interval, $2 interval) returns interval;
create FUNCTION interval_mul($1 interval, $2 double precision) returns interval;
create FUNCTION interval_ne($1 interval, $2 interval) returns boolean;
create FUNCTION interval_out($1 interval) returns cstring;
create FUNCTION interval_pl_date($1 interval, $2 date) returns timestamp;
create FUNCTION interval_pl_time($1 interval, $2 time) returns time;
create FUNCTION interval_pl_timestamp($1 interval, $2 timestamp) returns timestamp;
create FUNCTION interval_pl_timestamptz($1 interval, $2 timestamp with time zone) returns timestamp with time zone;
create FUNCTION interval_pl_timetz($1 interval, $2 time with time zone) returns time with time zone;
create FUNCTION interval_pl($1 interval, $2 interval) returns interval;
create FUNCTION interval_recv($1 internal, $2 oid, $3 integer) returns interval;
create FUNCTION interval_send($1 interval) returns bytea;
create FUNCTION interval_smaller($1 interval, $2 interval) returns interval;
create FUNCTION interval_transform($1 internal) returns internal;
create FUNCTION interval_um($1 interval) returns interval;
create FUNCTION "interval"($1 interval, $2 integer) returns interval;
create FUNCTION "interval"($1 reltime) returns interval;
create FUNCTION "interval"($1 time) returns interval;
create FUNCTION intervaltypmodin($1 cstring[]) returns integer;
create FUNCTION intervaltypmodout($1 integer) returns cstring;
create FUNCTION intinterval($1 abstime, $2 tinterval) returns boolean;
create FUNCTION isclosed($1 path) returns boolean;
create FUNCTION isempty($1 anyrange) returns boolean;
create FUNCTION isfinite($1 abstime) returns boolean;
create FUNCTION isfinite($1 date) returns boolean;
create FUNCTION isfinite($1 interval) returns boolean;
create FUNCTION isfinite($1 timestamp) returns boolean;
create FUNCTION isfinite($1 timestamp with time zone) returns boolean;
create FUNCTION ishorizontal($1 line) returns boolean;
create FUNCTION ishorizontal($1 lseg) returns boolean;
create FUNCTION ishorizontal($1 point, $2 point) returns boolean;
create FUNCTION iso8859_1_to_utf8($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION iso8859_to_utf8($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION iso_to_koi8r($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION iso_to_mic($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION iso_to_win1251($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION iso_to_win866($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION isopen($1 path) returns boolean;
create FUNCTION isparallel($1 line, $2 line) returns boolean;
create FUNCTION isparallel($1 lseg, $2 lseg) returns boolean;
create FUNCTION isperp($1 line, $2 line) returns boolean;
create FUNCTION isperp($1 lseg, $2 lseg) returns boolean;
create FUNCTION isvertical($1 line) returns boolean;
create FUNCTION isvertical($1 lseg) returns boolean;
create FUNCTION isvertical($1 point, $2 point) returns boolean;
create FUNCTION johab_to_utf8($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION json_agg_finalfn($1 internal) returns json;
create FUNCTION json_agg_transfn($1 internal, $2 anyelement) returns internal;
create FUNCTION json_agg($1 anyelement) returns json;
create FUNCTION json_array_element_text(from_json json, element_index integer) returns text;
create FUNCTION json_array_element(from_json json, element_index integer) returns json;
create FUNCTION json_array_elements_text(from_json json, "value" text) returns setof text;
create FUNCTION json_array_elements(from_json json, "value" json) returns setof json;
create FUNCTION json_array_length($1 json) returns integer;
create FUNCTION json_build_array();
create FUNCTION json_build_array($1 "any") returns json;
create FUNCTION json_build_object();
create FUNCTION json_build_object($1 "any") returns json;
create FUNCTION json_each_text(from_json json, "key" text, "value" text) returns setof record;
create FUNCTION json_each(from_json json, "key" text, "value" json) returns setof record;
create FUNCTION json_extract_path_text(from_json json, path_elems text[]) returns text;
create FUNCTION json_extract_path(from_json json, path_elems text[]) returns json;
create FUNCTION json_in($1 cstring) returns json;
create FUNCTION json_object_agg($1 "any", $2 "any") returns json;
create FUNCTION json_object_agg_finalfn($1 internal) returns json;
create FUNCTION json_object_agg_transfn($1 internal, $2 "any", $3 "any") returns internal;
create FUNCTION json_object_field_text(from_json json, field_name text) returns text;
create FUNCTION json_object_field(from_json json, field_name text) returns json;
create FUNCTION json_object_keys($1 json) returns setof text;
create FUNCTION json_object($1 text[]) returns json;
create FUNCTION json_object($1 text[], $2 text[]) returns json;
create FUNCTION json_out($1 json) returns cstring;
create FUNCTION json_populate_record(base anyelement, from_json json, use_json_as_text boolean) returns anyelement;
create FUNCTION json_populate_recordset(base anyelement, from_json json, use_json_as_text boolean) returns setof anyelement;
create FUNCTION json_recv($1 internal) returns json;
create FUNCTION json_send($1 json) returns bytea;
create FUNCTION json_to_record($1 json) returns record;
create FUNCTION json_to_recordset($1 json) returns setof record;
create FUNCTION json_typeof($1 json) returns text;
create FUNCTION jsonb_array_element_text(from_json jsonb, element_index integer) returns text;
create FUNCTION jsonb_array_element(from_json jsonb, element_index integer) returns jsonb;
create FUNCTION jsonb_array_elements_text(from_json jsonb, "value" text) returns setof text;
create FUNCTION jsonb_array_elements(from_json jsonb, "value" jsonb) returns setof jsonb;
create FUNCTION jsonb_array_length($1 jsonb) returns integer;
create FUNCTION jsonb_cmp($1 jsonb, $2 jsonb) returns integer;
create FUNCTION jsonb_contained($1 jsonb, $2 jsonb) returns boolean;
create FUNCTION jsonb_contains($1 jsonb, $2 jsonb) returns boolean;
create FUNCTION jsonb_each_text(from_json jsonb, "key" text, "value" text) returns setof record;
create FUNCTION jsonb_each(from_json jsonb, "key" text, "value" jsonb) returns setof record;
create FUNCTION jsonb_eq($1 jsonb, $2 jsonb) returns boolean;
create FUNCTION jsonb_exists_all($1 jsonb, $2 text[]) returns boolean;
create FUNCTION jsonb_exists_any($1 jsonb, $2 text[]) returns boolean;
create FUNCTION jsonb_exists($1 jsonb, $2 text) returns boolean;
create FUNCTION jsonb_extract_path_text(from_json jsonb, path_elems text[]) returns text;
create FUNCTION jsonb_extract_path(from_json jsonb, path_elems text[]) returns jsonb;
create FUNCTION jsonb_ge($1 jsonb, $2 jsonb) returns boolean;
create FUNCTION jsonb_gt($1 jsonb, $2 jsonb) returns boolean;
create FUNCTION jsonb_hash($1 jsonb) returns integer;
create FUNCTION jsonb_in($1 cstring) returns jsonb;
create FUNCTION jsonb_le($1 jsonb, $2 jsonb) returns boolean;
create FUNCTION jsonb_lt($1 jsonb, $2 jsonb) returns boolean;
create FUNCTION jsonb_ne($1 jsonb, $2 jsonb) returns boolean;
create FUNCTION jsonb_object_field_text(from_json jsonb, field_name text) returns text;
create FUNCTION jsonb_object_field(from_json jsonb, field_name text) returns jsonb;
create FUNCTION jsonb_object_keys($1 jsonb) returns setof text;
create FUNCTION jsonb_out($1 jsonb) returns cstring;
create FUNCTION jsonb_populate_record($1 anyelement, $2 jsonb) returns anyelement;
create FUNCTION jsonb_populate_recordset($1 anyelement, $2 jsonb) returns setof anyelement;
create FUNCTION jsonb_recv($1 internal) returns jsonb;
create FUNCTION jsonb_send($1 jsonb) returns bytea;
create FUNCTION jsonb_to_record($1 jsonb) returns record;
create FUNCTION jsonb_to_recordset($1 jsonb) returns setof record;
create FUNCTION jsonb_typeof($1 jsonb) returns text;
create FUNCTION justify_days($1 interval) returns interval;
create FUNCTION justify_hours($1 interval) returns interval;
create FUNCTION justify_interval($1 interval) returns interval;
create FUNCTION koi8r_to_iso($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION koi8r_to_mic($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION koi8r_to_utf8($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION koi8r_to_win1251($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION koi8r_to_win866($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION koi8u_to_utf8($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION lag($1 anyelement) returns anyelement;
create FUNCTION lag($1 anyelement, $2 integer) returns anyelement;
create FUNCTION lag($1 anyelement, $2 integer, $3 anyelement) returns anyelement;
create FUNCTION language_handler_in($1 cstring) returns language_handler;
create FUNCTION language_handler_out($1 language_handler) returns cstring;
create FUNCTION last_value($1 anyelement) returns anyelement;
create FUNCTION lastval();
create FUNCTION latin1_to_mic($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION latin2_to_mic($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION latin2_to_win1250($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION latin3_to_mic($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION latin4_to_mic($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION lead($1 anyelement) returns anyelement;
create FUNCTION lead($1 anyelement, $2 integer) returns anyelement;
create FUNCTION lead($1 anyelement, $2 integer, $3 anyelement) returns anyelement;
create FUNCTION "left"($1 text, $2 integer) returns text;
create FUNCTION length($1 bit) returns integer;
create FUNCTION length($1 bytea) returns integer;
create FUNCTION length($1 bytea, $2 name) returns integer;
create FUNCTION length($1 char) returns integer;
create FUNCTION length($1 lseg) returns double precision;
create FUNCTION length($1 path) returns double precision;
create FUNCTION length($1 text) returns integer;
create FUNCTION length($1 tsvector) returns integer;
create FUNCTION like_escape($1 bytea, $2 bytea) returns bytea;
create FUNCTION like_escape($1 text, $2 text) returns text;
create FUNCTION "like"($1 bytea, $2 bytea) returns boolean;
create FUNCTION likejoinsel($1 internal, $2 oid, $3 internal, $4 smallint, $5 internal) returns double precision;
create FUNCTION "like"($1 name, $2 text) returns boolean;
create FUNCTION likesel($1 internal, $2 oid, $3 internal, $4 integer) returns double precision;
create FUNCTION "like"($1 text, $2 text) returns boolean;
create FUNCTION line_distance($1 line, $2 line) returns double precision;
create FUNCTION line_eq($1 line, $2 line) returns boolean;
create FUNCTION line_horizontal($1 line) returns boolean;
create FUNCTION line_in($1 cstring) returns line;
create FUNCTION line_interpt($1 line, $2 line) returns point;
create FUNCTION line_intersect($1 line, $2 line) returns boolean;
create FUNCTION line_out($1 line) returns cstring;
create FUNCTION line_parallel($1 line, $2 line) returns boolean;
create FUNCTION line_perp($1 line, $2 line) returns boolean;
create FUNCTION line_recv($1 internal) returns line;
create FUNCTION line_send($1 line) returns bytea;
create FUNCTION line_vertical($1 line) returns boolean;
create FUNCTION line($1 point, $2 point) returns line;
create FUNCTION ln($1 double precision) returns double precision;
create FUNCTION ln($1 numeric) returns numeric;
create FUNCTION lo_close($1 integer) returns integer;
create FUNCTION lo_create($1 oid) returns oid;
create FUNCTION lo_creat($1 integer) returns oid;
create FUNCTION lo_export($1 oid, $2 text) returns integer;
create FUNCTION lo_from_bytea($1 oid, $2 bytea) returns oid;
create FUNCTION lo_get($1 oid) returns bytea;
create FUNCTION lo_get($1 oid, $2 bigint, $3 integer) returns bytea;
create FUNCTION lo_import($1 text) returns oid;
create FUNCTION lo_import($1 text, $2 oid) returns oid;
create FUNCTION lo_lseek64($1 integer, $2 bigint, $3 integer) returns bigint;
create FUNCTION lo_lseek($1 integer, $2 integer, $3 integer) returns integer;
create FUNCTION lo_open($1 oid, $2 integer) returns integer;
create FUNCTION lo_put($1 oid, $2 bigint, $3 bytea) returns void;
create FUNCTION lo_tell64($1 integer) returns bigint;
create FUNCTION lo_tell($1 integer) returns integer;
create FUNCTION lo_truncate64($1 integer, $2 bigint) returns integer;
create FUNCTION lo_truncate($1 integer, $2 integer) returns integer;
create FUNCTION lo_unlink($1 oid) returns integer;
create FUNCTION log($1 double precision) returns double precision;
create FUNCTION log($1 numeric) returns numeric;
create FUNCTION log($1 numeric, $2 numeric) returns numeric;
create FUNCTION loread($1 integer, $2 integer) returns bytea;
create FUNCTION lower_inc($1 anyrange) returns boolean;
create FUNCTION lower_inf($1 anyrange) returns boolean;
create FUNCTION "lower"($1 anyrange) returns anyelement;
create FUNCTION "lower"($1 text) returns text;
create FUNCTION lowrite($1 integer, $2 bytea) returns integer;
create FUNCTION lpad($1 text, $2 integer) returns text;
create FUNCTION lpad($1 text, $2 integer, $3 text) returns text;
create FUNCTION lseg_center($1 lseg) returns point;
create FUNCTION lseg_distance($1 lseg, $2 lseg) returns double precision;
create FUNCTION lseg_eq($1 lseg, $2 lseg) returns boolean;
create FUNCTION lseg_ge($1 lseg, $2 lseg) returns boolean;
create FUNCTION lseg_gt($1 lseg, $2 lseg) returns boolean;
create FUNCTION lseg_horizontal($1 lseg) returns boolean;
create FUNCTION lseg_in($1 cstring) returns lseg;
create FUNCTION lseg_interpt($1 lseg, $2 lseg) returns point;
create FUNCTION lseg_intersect($1 lseg, $2 lseg) returns boolean;
create FUNCTION lseg_le($1 lseg, $2 lseg) returns boolean;
create FUNCTION lseg_length($1 lseg) returns double precision;
create FUNCTION lseg_lt($1 lseg, $2 lseg) returns boolean;
create FUNCTION lseg_ne($1 lseg, $2 lseg) returns boolean;
create FUNCTION lseg_out($1 lseg) returns cstring;
create FUNCTION lseg_parallel($1 lseg, $2 lseg) returns boolean;
create FUNCTION lseg_perp($1 lseg, $2 lseg) returns boolean;
create FUNCTION lseg_recv($1 internal) returns lseg;
create FUNCTION lseg_send($1 lseg) returns bytea;
create FUNCTION lseg_vertical($1 lseg) returns boolean;
create FUNCTION lseg($1 box) returns lseg;
create FUNCTION lseg($1 point, $2 point) returns lseg;
create FUNCTION ltrim($1 text) returns text;
create FUNCTION ltrim($1 text, $2 text) returns text;
create FUNCTION macaddr_and($1 macaddr, $2 macaddr) returns macaddr;
create FUNCTION macaddr_cmp($1 macaddr, $2 macaddr) returns integer;
create FUNCTION macaddr_eq($1 macaddr, $2 macaddr) returns boolean;
create FUNCTION macaddr_ge($1 macaddr, $2 macaddr) returns boolean;
create FUNCTION macaddr_gt($1 macaddr, $2 macaddr) returns boolean;
create FUNCTION macaddr_in($1 cstring) returns macaddr;
create FUNCTION macaddr_le($1 macaddr, $2 macaddr) returns boolean;
create FUNCTION macaddr_lt($1 macaddr, $2 macaddr) returns boolean;
create FUNCTION macaddr_ne($1 macaddr, $2 macaddr) returns boolean;
create FUNCTION macaddr_not($1 macaddr) returns macaddr;
create FUNCTION macaddr_or($1 macaddr, $2 macaddr) returns macaddr;
create FUNCTION macaddr_out($1 macaddr) returns cstring;
create FUNCTION macaddr_recv($1 internal) returns macaddr;
create FUNCTION macaddr_send($1 macaddr) returns bytea;
create FUNCTION make_date("year" integer, "month" integer, "day" integer) returns date;
create FUNCTION make_interval(years integer, months integer, weeks integer, days integer, hours integer, mins integer, secs double precision) returns interval;
create FUNCTION make_time("hour" integer, "min" integer, sec double precision) returns time;
create FUNCTION make_timestamp("year" integer, "month" integer, mday integer, "hour" integer, "min" integer, sec double precision) returns timestamp;
create FUNCTION make_timestamptz("year" integer, "month" integer, mday integer, "hour" integer, "min" integer, sec double precision) returns timestamp with time zone;
create FUNCTION make_timestamptz("year" integer, "month" integer, mday integer, "hour" integer, "min" integer, sec double precision, timezone text) returns timestamp with time zone;
create FUNCTION makeaclitem($1 oid, $2 oid, $3 text, $4 boolean) returns aclitem;
create FUNCTION masklen($1 inet) returns integer;
create FUNCTION "max"($1 abstime) returns abstime;
create FUNCTION "max"($1 anyarray) returns anyarray;
create FUNCTION "max"($1 anyenum) returns anyenum;
create FUNCTION "max"($1 bigint) returns bigint;
create FUNCTION "max"($1 char) returns char;
create FUNCTION "max"($1 date) returns date;
create FUNCTION "max"($1 double precision) returns double precision;
create FUNCTION "max"($1 integer) returns integer;
create FUNCTION "max"($1 interval) returns interval;
create FUNCTION "max"($1 money) returns money;
create FUNCTION "max"($1 numeric) returns numeric;
create FUNCTION "max"($1 oid) returns oid;
create FUNCTION "max"($1 real) returns real;
create FUNCTION "max"($1 smallint) returns smallint;
create FUNCTION "max"($1 text) returns text;
create FUNCTION "max"($1 tid) returns tid;
create FUNCTION "max"($1 time) returns time;
create FUNCTION "max"($1 time with time zone) returns time with time zone;
create FUNCTION "max"($1 timestamp) returns timestamp;
create FUNCTION "max"($1 timestamp with time zone) returns timestamp with time zone;
create FUNCTION md5($1 bytea) returns text;
create FUNCTION md5($1 text) returns text;
create FUNCTION mic_to_ascii($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION mic_to_big5($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION mic_to_euc_cn($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION mic_to_euc_jp($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION mic_to_euc_kr($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION mic_to_euc_tw($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION mic_to_iso($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION mic_to_koi8r($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION mic_to_latin1($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION mic_to_latin2($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION mic_to_latin3($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION mic_to_latin4($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION mic_to_sjis($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION mic_to_win1250($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION mic_to_win1251($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION mic_to_win866($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION "min"($1 abstime) returns abstime;
create FUNCTION "min"($1 anyarray) returns anyarray;
create FUNCTION "min"($1 anyenum) returns anyenum;
create FUNCTION "min"($1 bigint) returns bigint;
create FUNCTION "min"($1 char) returns char;
create FUNCTION "min"($1 date) returns date;
create FUNCTION "min"($1 double precision) returns double precision;
create FUNCTION "min"($1 integer) returns integer;
create FUNCTION "min"($1 interval) returns interval;
create FUNCTION "min"($1 money) returns money;
create FUNCTION "min"($1 numeric) returns numeric;
create FUNCTION "min"($1 oid) returns oid;
create FUNCTION "min"($1 real) returns real;
create FUNCTION "min"($1 smallint) returns smallint;
create FUNCTION "min"($1 text) returns text;
create FUNCTION "min"($1 tid) returns tid;
create FUNCTION "min"($1 time) returns time;
create FUNCTION "min"($1 time with time zone) returns time with time zone;
create FUNCTION "min"($1 timestamp) returns timestamp;
create FUNCTION "min"($1 timestamp with time zone) returns timestamp with time zone;
create FUNCTION mktinterval($1 abstime, $2 abstime) returns tinterval;
create FUNCTION mod($1 bigint, $2 bigint) returns bigint;
create FUNCTION mode_final($1 internal, $2 anyelement) returns anyelement;
create FUNCTION mode($1 anyelement) returns anyelement;
create FUNCTION mod($1 integer, $2 integer) returns integer;
create FUNCTION mod($1 numeric, $2 numeric) returns numeric;
create FUNCTION mod($1 smallint, $2 smallint) returns smallint;
create FUNCTION money($1 bigint) returns money;
create FUNCTION money($1 integer) returns money;
create FUNCTION money($1 numeric) returns money;
create FUNCTION mul_d_interval($1 double precision, $2 interval) returns interval;
create FUNCTION name($1 char) returns name;
create FUNCTION nameeq($1 name, $2 name) returns boolean;
create FUNCTION namege($1 name, $2 name) returns boolean;
create FUNCTION namegt($1 name, $2 name) returns boolean;
create FUNCTION nameiclike($1 name, $2 text) returns boolean;
create FUNCTION nameicnlike($1 name, $2 text) returns boolean;
create FUNCTION nameicregexeq($1 name, $2 text) returns boolean;
create FUNCTION nameicregexne($1 name, $2 text) returns boolean;
create FUNCTION namein($1 cstring) returns name;
create FUNCTION namele($1 name, $2 name) returns boolean;
create FUNCTION namelike($1 name, $2 text) returns boolean;
create FUNCTION namelt($1 name, $2 name) returns boolean;
create FUNCTION namene($1 name, $2 name) returns boolean;
create FUNCTION namenlike($1 name, $2 text) returns boolean;
create FUNCTION nameout($1 name) returns cstring;
create FUNCTION namerecv($1 internal) returns name;
create FUNCTION nameregexeq($1 name, $2 text) returns boolean;
create FUNCTION nameregexne($1 name, $2 text) returns boolean;
create FUNCTION namesend($1 name) returns bytea;
create FUNCTION name($1 text) returns name;
create FUNCTION name($1 varchar) returns name;
create FUNCTION neqjoinsel($1 internal, $2 oid, $3 internal, $4 smallint, $5 internal) returns double precision;
create FUNCTION neqsel($1 internal, $2 oid, $3 internal, $4 integer) returns double precision;
create FUNCTION netmask($1 inet) returns inet;
create FUNCTION network_cmp($1 inet, $2 inet) returns integer;
create FUNCTION network_eq($1 inet, $2 inet) returns boolean;
create FUNCTION network_ge($1 inet, $2 inet) returns boolean;
create FUNCTION network_gt($1 inet, $2 inet) returns boolean;
create FUNCTION network_le($1 inet, $2 inet) returns boolean;
create FUNCTION network_lt($1 inet, $2 inet) returns boolean;
create FUNCTION network_ne($1 inet, $2 inet) returns boolean;
create FUNCTION network_overlap($1 inet, $2 inet) returns boolean;
create FUNCTION network_subeq($1 inet, $2 inet) returns boolean;
create FUNCTION network_sub($1 inet, $2 inet) returns boolean;
create FUNCTION network_supeq($1 inet, $2 inet) returns boolean;
create FUNCTION network_sup($1 inet, $2 inet) returns boolean;
create FUNCTION network($1 inet) returns cidr;
create FUNCTION networkjoinsel($1 internal, $2 oid, $3 internal, $4 smallint, $5 internal) returns double precision;
create FUNCTION networksel($1 internal, $2 oid, $3 internal, $4 integer) returns double precision;
create FUNCTION nextval($1 regclass) returns bigint;
create FUNCTION nlikejoinsel($1 internal, $2 oid, $3 internal, $4 smallint, $5 internal) returns double precision;
create FUNCTION nlikesel($1 internal, $2 oid, $3 internal, $4 integer) returns double precision;
create FUNCTION notlike($1 bytea, $2 bytea) returns boolean;
create FUNCTION notlike($1 name, $2 text) returns boolean;
create FUNCTION notlike($1 text, $2 text) returns boolean;
create FUNCTION now();
create FUNCTION npoints($1 path) returns integer;
create FUNCTION npoints($1 polygon) returns integer;
create FUNCTION nth_value($1 anyelement, $2 integer) returns anyelement;
create FUNCTION ntile($1 integer) returns integer;
create FUNCTION numeric_abs($1 numeric) returns numeric;
create FUNCTION numeric_accum_inv($1 internal, $2 numeric) returns internal;
create FUNCTION numeric_accum($1 internal, $2 numeric) returns internal;
create FUNCTION numeric_add($1 numeric, $2 numeric) returns numeric;
create FUNCTION numeric_avg_accum($1 internal, $2 numeric) returns internal;
create FUNCTION numeric_avg($1 internal) returns numeric;
create FUNCTION numeric_cmp($1 numeric, $2 numeric) returns integer;
create FUNCTION numeric_div_trunc($1 numeric, $2 numeric) returns numeric;
create FUNCTION numeric_div($1 numeric, $2 numeric) returns numeric;
create FUNCTION numeric_eq($1 numeric, $2 numeric) returns boolean;
create FUNCTION numeric_exp($1 numeric) returns numeric;
create FUNCTION numeric_fac($1 bigint) returns numeric;
create FUNCTION numeric_ge($1 numeric, $2 numeric) returns boolean;
create FUNCTION numeric_gt($1 numeric, $2 numeric) returns boolean;
create FUNCTION numeric_inc($1 numeric) returns numeric;
create FUNCTION numeric_in($1 cstring, $2 oid, $3 integer) returns numeric;
create FUNCTION numeric_larger($1 numeric, $2 numeric) returns numeric;
create FUNCTION numeric_le($1 numeric, $2 numeric) returns boolean;
create FUNCTION numeric_ln($1 numeric) returns numeric;
create FUNCTION numeric_log($1 numeric, $2 numeric) returns numeric;
create FUNCTION numeric_lt($1 numeric, $2 numeric) returns boolean;
create FUNCTION numeric_mod($1 numeric, $2 numeric) returns numeric;
create FUNCTION numeric_mul($1 numeric, $2 numeric) returns numeric;
create FUNCTION numeric_ne($1 numeric, $2 numeric) returns boolean;
create FUNCTION numeric_out($1 numeric) returns cstring;
create FUNCTION numeric_power($1 numeric, $2 numeric) returns numeric;
create FUNCTION numeric_recv($1 internal, $2 oid, $3 integer) returns numeric;
create FUNCTION numeric_send($1 numeric) returns bytea;
create FUNCTION numeric_smaller($1 numeric, $2 numeric) returns numeric;
create FUNCTION numeric_sqrt($1 numeric) returns numeric;
create FUNCTION numeric_stddev_pop($1 internal) returns numeric;
create FUNCTION numeric_stddev_samp($1 internal) returns numeric;
create FUNCTION numeric_sub($1 numeric, $2 numeric) returns numeric;
create FUNCTION numeric_sum($1 internal) returns numeric;
create FUNCTION numeric_transform($1 internal) returns internal;
create FUNCTION numeric_uminus($1 numeric) returns numeric;
create FUNCTION numeric_uplus($1 numeric) returns numeric;
create FUNCTION numeric_var_pop($1 internal) returns numeric;
create FUNCTION numeric_var_samp($1 internal) returns numeric;
create FUNCTION "numeric"($1 bigint) returns numeric;
create FUNCTION "numeric"($1 double precision) returns numeric;
create FUNCTION "numeric"($1 integer) returns numeric;
create FUNCTION "numeric"($1 money) returns numeric;
create FUNCTION "numeric"($1 numeric, $2 integer) returns numeric;
create FUNCTION "numeric"($1 real) returns numeric;
create FUNCTION "numeric"($1 smallint) returns numeric;
create FUNCTION numerictypmodin($1 cstring[]) returns integer;
create FUNCTION numerictypmodout($1 integer) returns cstring;
create FUNCTION numnode($1 tsquery) returns integer;
create FUNCTION numrange_subdiff($1 numeric, $2 numeric) returns double precision;
create FUNCTION numrange($1 numeric, $2 numeric) returns numrange;
create FUNCTION numrange($1 numeric, $2 numeric, $3 text) returns numrange;
create FUNCTION obj_description($1 oid) returns text;
create FUNCTION obj_description($1 oid, $2 name) returns text;
create FUNCTION "octet_length"($1 bit) returns integer;
create FUNCTION "octet_length"($1 bytea) returns integer;
create FUNCTION "octet_length"($1 char) returns integer;
create FUNCTION "octet_length"($1 text) returns integer;
create FUNCTION oid($1 bigint) returns oid;
create FUNCTION oideq($1 oid, $2 oid) returns boolean;
create FUNCTION oidge($1 oid, $2 oid) returns boolean;
create FUNCTION oidgt($1 oid, $2 oid) returns boolean;
create FUNCTION oidin($1 cstring) returns oid;
create FUNCTION oidlarger($1 oid, $2 oid) returns oid;
create FUNCTION oidle($1 oid, $2 oid) returns boolean;
create FUNCTION oidlt($1 oid, $2 oid) returns boolean;
create FUNCTION oidne($1 oid, $2 oid) returns boolean;
create FUNCTION oidout($1 oid) returns cstring;
create FUNCTION oidrecv($1 internal) returns oid;
create FUNCTION oidsend($1 oid) returns bytea;
create FUNCTION oidsmaller($1 oid, $2 oid) returns oid;
create FUNCTION oidvectoreq($1 oidvector, $2 oidvector) returns boolean;
create FUNCTION oidvectorge($1 oidvector, $2 oidvector) returns boolean;
create FUNCTION oidvectorgt($1 oidvector, $2 oidvector) returns boolean;
create FUNCTION oidvectorin($1 cstring) returns oidvector;
create FUNCTION oidvectorle($1 oidvector, $2 oidvector) returns boolean;
create FUNCTION oidvectorlt($1 oidvector, $2 oidvector) returns boolean;
create FUNCTION oidvectorne($1 oidvector, $2 oidvector) returns boolean;
create FUNCTION oidvectorout($1 oidvector) returns cstring;
create FUNCTION oidvectorrecv($1 internal) returns oidvector;
create FUNCTION oidvectorsend($1 oidvector) returns bytea;
create FUNCTION oidvectortypes($1 oidvector) returns text;
create FUNCTION on_pb($1 point, $2 box) returns boolean;
create FUNCTION on_pl($1 point, $2 line) returns boolean;
create FUNCTION on_ppath($1 point, $2 path) returns boolean;
create FUNCTION on_ps($1 point, $2 lseg) returns boolean;
create FUNCTION on_sb($1 lseg, $2 box) returns boolean;
create FUNCTION on_sl($1 lseg, $2 line) returns boolean;
create FUNCTION opaque_in($1 cstring) returns opaque;
create FUNCTION opaque_out($1 opaque) returns cstring;
create FUNCTION ordered_set_transition_multi($1 internal, $2 "any") returns internal;
create FUNCTION ordered_set_transition($1 internal, $2 "any") returns internal;
create FUNCTION "overlaps"($1 time with time zone, $2 time with time zone, $3 time with time zone, $4 time with time zone) returns boolean;
create FUNCTION "overlaps"($1 time, $2 interval, $3 time, $4 interval) returns boolean;
create FUNCTION "overlaps"($1 time, $2 interval, $3 time, $4 time) returns boolean;
create FUNCTION "overlaps"($1 timestamp with time zone, $2 interval, $3 timestamp with time zone, $4 interval) returns boolean;
create FUNCTION "overlaps"($1 timestamp with time zone, $2 interval, $3 timestamp with time zone, $4 timestamp with time zone) returns boolean;
create FUNCTION "overlaps"($1 timestamp with time zone, $2 timestamp with time zone, $3 timestamp with time zone, $4 interval) returns boolean;
create FUNCTION "overlaps"($1 timestamp with time zone, $2 timestamp with time zone, $3 timestamp with time zone, $4 timestamp with time zone) returns boolean;
create FUNCTION "overlaps"($1 timestamp, $2 interval, $3 timestamp, $4 interval) returns boolean;
create FUNCTION "overlaps"($1 timestamp, $2 interval, $3 timestamp, $4 timestamp) returns boolean;
create FUNCTION "overlaps"($1 timestamp, $2 timestamp, $3 timestamp, $4 interval) returns boolean;
create FUNCTION "overlaps"($1 timestamp, $2 timestamp, $3 timestamp, $4 timestamp) returns boolean;
create FUNCTION "overlaps"($1 time, $2 time, $3 time, $4 interval) returns boolean;
create FUNCTION "overlaps"($1 time, $2 time, $3 time, $4 time) returns boolean;
create FUNCTION overlay($1 bit, $2 bit, $3 integer) returns bit;
create FUNCTION overlay($1 bit, $2 bit, $3 integer, $4 integer) returns bit;
create FUNCTION overlay($1 bytea, $2 bytea, $3 integer) returns bytea;
create FUNCTION overlay($1 bytea, $2 bytea, $3 integer, $4 integer) returns bytea;
create FUNCTION overlay($1 text, $2 text, $3 integer) returns text;
create FUNCTION overlay($1 text, $2 text, $3 integer, $4 integer) returns text;
create FUNCTION path_add_pt($1 path, $2 point) returns path;
create FUNCTION path_add($1 path, $2 path) returns path;
create FUNCTION path_center($1 path) returns point;
create FUNCTION path_contain_pt($1 path, $2 point) returns boolean;
create FUNCTION path_distance($1 path, $2 path) returns double precision;
create FUNCTION path_div_pt($1 path, $2 point) returns path;
create FUNCTION path_in($1 cstring) returns path;
create FUNCTION path_inter($1 path, $2 path) returns boolean;
create FUNCTION path_length($1 path) returns double precision;
create FUNCTION path_mul_pt($1 path, $2 point) returns path;
create FUNCTION path_n_eq($1 path, $2 path) returns boolean;
create FUNCTION path_n_ge($1 path, $2 path) returns boolean;
create FUNCTION path_n_gt($1 path, $2 path) returns boolean;
create FUNCTION path_n_le($1 path, $2 path) returns boolean;
create FUNCTION path_n_lt($1 path, $2 path) returns boolean;
create FUNCTION path_npoints($1 path) returns integer;
create FUNCTION path_out($1 path) returns cstring;
create FUNCTION path_recv($1 internal) returns path;
create FUNCTION path_send($1 path) returns bytea;
create FUNCTION path_sub_pt($1 path, $2 point) returns path;
create FUNCTION path($1 polygon) returns path;
create FUNCTION pclose($1 path) returns path;
create FUNCTION percent_rank();
create FUNCTION percent_rank($1 "any") returns double precision;
create FUNCTION percent_rank_final($1 internal, $2 "any") returns double precision;
create FUNCTION percentile_cont_float8_final($1 internal, $2 double precision) returns double precision;
create FUNCTION percentile_cont_float8_multi_final($1 internal, $2 double precision[]) returns double precision[];
create FUNCTION percentile_cont_interval_final($1 internal, $2 double precision) returns interval;
create FUNCTION percentile_cont_interval_multi_final($1 internal, $2 double precision[]) returns interval[];
create FUNCTION percentile_cont($1 double precision[], $2 double precision) returns double precision[];
create FUNCTION percentile_cont($1 double precision[], $2 interval) returns interval[];
create FUNCTION percentile_cont($1 double precision, $2 double precision) returns double precision;
create FUNCTION percentile_cont($1 double precision, $2 interval) returns interval;
create FUNCTION percentile_disc_final($1 internal, $2 double precision, $3 anyelement) returns anyelement;
create FUNCTION percentile_disc_multi_final($1 internal, $2 double precision[], $3 anyelement) returns anyarray;
create FUNCTION percentile_disc($1 double precision[], $2 anyelement) returns anyarray;
create FUNCTION percentile_disc($1 double precision, $2 anyelement) returns anyelement;
create FUNCTION pg_advisory_lock_shared($1 bigint) returns void;
create FUNCTION pg_advisory_lock_shared($1 integer, $2 integer) returns void;
create FUNCTION pg_advisory_lock($1 bigint) returns void;
create FUNCTION pg_advisory_lock($1 integer, $2 integer) returns void;
create FUNCTION pg_advisory_unlock_all();
create FUNCTION pg_advisory_unlock_shared($1 bigint) returns boolean;
create FUNCTION pg_advisory_unlock_shared($1 integer, $2 integer) returns boolean;
create FUNCTION pg_advisory_unlock($1 bigint) returns boolean;
create FUNCTION pg_advisory_unlock($1 integer, $2 integer) returns boolean;
create FUNCTION pg_advisory_xact_lock_shared($1 bigint) returns void;
create FUNCTION pg_advisory_xact_lock_shared($1 integer, $2 integer) returns void;
create FUNCTION pg_advisory_xact_lock($1 bigint) returns void;
create FUNCTION pg_advisory_xact_lock($1 integer, $2 integer) returns void;
create FUNCTION pg_available_extension_versions(name name, version text, superuser boolean, relocatable boolean, "schema" name, requires name[], comment text) returns setof record;
create FUNCTION pg_available_extensions(name name, default_version text, comment text) returns setof record;
create FUNCTION pg_backend_pid();
create FUNCTION pg_backup_start_time();
create FUNCTION pg_cancel_backend($1 integer) returns boolean;
create FUNCTION pg_char_to_encoding($1 name) returns integer;
create FUNCTION pg_client_encoding();
create FUNCTION pg_collation_for($1 "any") returns text;
create FUNCTION pg_collation_is_visible($1 oid) returns boolean;
create FUNCTION pg_column_is_updatable($1 regclass, $2 smallint, $3 boolean) returns boolean;
create FUNCTION pg_column_size($1 "any") returns integer;
create FUNCTION pg_conf_load_time();
create FUNCTION pg_conversion_is_visible($1 oid) returns boolean;
create FUNCTION pg_create_logical_replication_slot(slot_name text, plugin name, xlog_position pg_lsn) returns record;
create FUNCTION pg_create_physical_replication_slot(slot_name name, xlog_position pg_lsn) returns record;
create FUNCTION pg_create_restore_point($1 text) returns pg_lsn;
create FUNCTION pg_current_xlog_insert_location();
create FUNCTION pg_current_xlog_location();
create FUNCTION pg_cursor(name text, statement text, is_holdable boolean, is_binary boolean, is_scrollable boolean, creation_time timestamp with time zone) returns setof record;
create FUNCTION pg_database_size($1 name) returns bigint;
create FUNCTION pg_database_size($1 oid) returns bigint;
create FUNCTION pg_describe_object($1 oid, $2 oid, $3 integer) returns text;
create FUNCTION pg_drop_replication_slot($1 name) returns void;
create FUNCTION pg_encoding_max_length($1 integer) returns integer;
create FUNCTION pg_encoding_to_char($1 integer) returns name;
create FUNCTION pg_event_trigger_dropped_objects(classid oid, objid oid, objsubid integer, object_type text, schema_name text, object_name text, object_identity text) returns setof record;
create FUNCTION pg_export_snapshot();
create FUNCTION pg_extension_config_dump($1 regclass, $2 text) returns void;
create FUNCTION pg_extension_update_paths(name name, source text, target text, path text) returns setof record;
create FUNCTION pg_filenode_relation($1 oid, $2 oid) returns regclass;
create FUNCTION pg_function_is_visible($1 oid) returns boolean;
create FUNCTION pg_get_constraintdef($1 oid) returns text;
create FUNCTION pg_get_constraintdef($1 oid, $2 boolean) returns text;
create FUNCTION pg_get_expr($1 pg_node_tree, $2 oid) returns text;
create FUNCTION pg_get_expr($1 pg_node_tree, $2 oid, $3 boolean) returns text;
create FUNCTION pg_get_function_arg_default($1 oid, $2 integer) returns text;
create FUNCTION pg_get_function_arguments($1 oid) returns text;
create FUNCTION pg_get_function_identity_arguments($1 oid) returns text;
create FUNCTION pg_get_function_result($1 oid) returns text;
create FUNCTION pg_get_functiondef($1 oid) returns text;
create FUNCTION pg_get_indexdef($1 oid) returns text;
create FUNCTION pg_get_indexdef($1 oid, $2 integer, $3 boolean) returns text;
create FUNCTION pg_get_keywords(word text, catcode "char", catdesc text) returns setof record;
create FUNCTION pg_get_multixact_members(multixid xid, xid xid, mode text) returns setof record;
create FUNCTION pg_get_replication_slots(slot_name name, plugin name, slot_type text, datoid oid, active boolean, xmin xid, catalog_xmin xid, restart_lsn pg_lsn) returns setof record;
create FUNCTION pg_get_ruledef($1 oid) returns text;
create FUNCTION pg_get_ruledef($1 oid, $2 boolean) returns text;
create FUNCTION pg_get_serial_sequence($1 text, $2 text) returns text;
create FUNCTION pg_get_triggerdef($1 oid) returns text;
create FUNCTION pg_get_triggerdef($1 oid, $2 boolean) returns text;
create FUNCTION pg_get_userbyid($1 oid) returns name;
create FUNCTION pg_get_viewdef($1 oid) returns text;
create FUNCTION pg_get_viewdef($1 oid, $2 boolean) returns text;
create FUNCTION pg_get_viewdef($1 oid, $2 integer) returns text;
create FUNCTION pg_get_viewdef($1 text) returns text;
create FUNCTION pg_get_viewdef($1 text, $2 boolean) returns text;
create FUNCTION pg_has_role($1 name, $2 name, $3 text) returns boolean;
create FUNCTION pg_has_role($1 name, $2 oid, $3 text) returns boolean;
create FUNCTION pg_has_role($1 name, $2 text) returns boolean;
create FUNCTION pg_has_role($1 oid, $2 name, $3 text) returns boolean;
create FUNCTION pg_has_role($1 oid, $2 oid, $3 text) returns boolean;
create FUNCTION pg_has_role($1 oid, $2 text) returns boolean;
create FUNCTION pg_identify_object(classid oid, objid oid, subobjid integer, type text, "schema" text, name text, "identity" text) returns record;
create FUNCTION pg_indexes_size($1 regclass) returns bigint;
create FUNCTION pg_is_in_backup();
create FUNCTION pg_is_in_recovery();
create FUNCTION pg_is_other_temp_schema($1 oid) returns boolean;
create FUNCTION pg_is_xlog_replay_paused();
create FUNCTION pg_last_xact_replay_timestamp();
create FUNCTION pg_last_xlog_receive_location();
create FUNCTION pg_last_xlog_replay_location();
create FUNCTION pg_listening_channels();
create FUNCTION pg_lock_status(locktype text, database oid, relation oid, page integer, tuple smallint, virtualxid text, transactionid xid, classid oid, objid oid, objsubid smallint, virtualtransaction text, pid integer, mode text, granted boolean, fastpath boolean) returns setof record;
create FUNCTION pg_logical_slot_get_binary_changes(slot_name name, upto_lsn pg_lsn, upto_nchanges integer, options text[], location pg_lsn, xid xid, data bytea) returns setof record;
create FUNCTION pg_logical_slot_get_changes(slot_name name, upto_lsn pg_lsn, upto_nchanges integer, options text[], location pg_lsn, xid xid, data text) returns setof record;
create FUNCTION pg_logical_slot_peek_binary_changes(slot_name name, upto_lsn pg_lsn, upto_nchanges integer, options text[], location pg_lsn, xid xid, data bytea) returns setof record;
create FUNCTION pg_logical_slot_peek_changes(slot_name name, upto_lsn pg_lsn, upto_nchanges integer, options text[], location pg_lsn, xid xid, data text) returns setof record;
create FUNCTION pg_ls_dir($1 text) returns setof text;
create FUNCTION pg_lsn_cmp($1 pg_lsn, $2 pg_lsn) returns integer;
create FUNCTION pg_lsn_eq($1 pg_lsn, $2 pg_lsn) returns boolean;
create FUNCTION pg_lsn_ge($1 pg_lsn, $2 pg_lsn) returns boolean;
create FUNCTION pg_lsn_gt($1 pg_lsn, $2 pg_lsn) returns boolean;
create FUNCTION pg_lsn_hash($1 pg_lsn) returns integer;
create FUNCTION pg_lsn_in($1 cstring) returns pg_lsn;
create FUNCTION pg_lsn_le($1 pg_lsn, $2 pg_lsn) returns boolean;
create FUNCTION pg_lsn_lt($1 pg_lsn, $2 pg_lsn) returns boolean;
create FUNCTION pg_lsn_mi($1 pg_lsn, $2 pg_lsn) returns numeric;
create FUNCTION pg_lsn_ne($1 pg_lsn, $2 pg_lsn) returns boolean;
create FUNCTION pg_lsn_out($1 pg_lsn) returns cstring;
create FUNCTION pg_lsn_recv($1 internal) returns pg_lsn;
create FUNCTION pg_lsn_send($1 pg_lsn) returns bytea;
create FUNCTION pg_my_temp_schema();
create FUNCTION pg_node_tree_in($1 cstring) returns pg_node_tree;
create FUNCTION pg_node_tree_out($1 pg_node_tree) returns cstring;
create FUNCTION pg_node_tree_recv($1 internal) returns pg_node_tree;
create FUNCTION pg_node_tree_send($1 pg_node_tree) returns bytea;
create FUNCTION pg_notify($1 text, $2 text) returns void;
create FUNCTION pg_opclass_is_visible($1 oid) returns boolean;
create FUNCTION pg_operator_is_visible($1 oid) returns boolean;
create FUNCTION pg_opfamily_is_visible($1 oid) returns boolean;
create FUNCTION pg_options_to_table(options_array text[], option_name text, option_value text) returns setof record;
create FUNCTION pg_postmaster_start_time();
create FUNCTION pg_prepared_statement(name text, statement text, prepare_time timestamp with time zone, parameter_types regtype[], from_sql boolean) returns setof record;
create FUNCTION pg_prepared_xact("transaction" xid, gid text, prepared timestamp with time zone, ownerid oid, dbid oid) returns setof record;
create FUNCTION pg_read_binary_file($1 text) returns bytea;
create FUNCTION pg_read_binary_file($1 text, $2 bigint, $3 bigint) returns bytea;
create FUNCTION pg_read_file($1 text) returns text;
create FUNCTION pg_read_file($1 text, $2 bigint, $3 bigint) returns text;
create FUNCTION pg_relation_filenode($1 regclass) returns oid;
create FUNCTION pg_relation_filepath($1 regclass) returns text;
create FUNCTION pg_relation_is_updatable($1 regclass, $2 boolean) returns integer;
create FUNCTION pg_relation_size($1 regclass) returns bigint;
create FUNCTION pg_relation_size($1 regclass, $2 text) returns bigint;
create FUNCTION pg_reload_conf();
create FUNCTION pg_rotate_logfile();
create FUNCTION pg_sequence_parameters(sequence_oid oid, start_value bigint, minimum_value bigint, maximum_value bigint, increment bigint, cycle_option boolean) returns record;
create FUNCTION pg_show_all_settings(name text, setting text, unit text, category text, short_desc text, extra_desc text, context text, vartype text, source text, min_val text, max_val text, enumvals text[], boot_val text, reset_val text, sourcefile text, sourceline integer) returns setof record;
create FUNCTION pg_size_pretty($1 bigint) returns text;
create FUNCTION pg_size_pretty($1 numeric) returns text;
create FUNCTION pg_sleep_for($1 interval) returns void;
create FUNCTION pg_sleep_until($1 timestamp with time zone) returns void;
create FUNCTION pg_sleep($1 double precision) returns void;
create FUNCTION pg_start_backup(label text, fast boolean) returns pg_lsn;
create FUNCTION pg_stat_clear_snapshot();
create FUNCTION pg_stat_file(filename text, "size" bigint, access timestamp with time zone, modification timestamp with time zone, change timestamp with time zone, creation timestamp with time zone, isdir boolean) returns record;
create FUNCTION pg_stat_get_activity(pid integer, datid oid, usesysid oid, application_name text, state text, query text, waiting boolean, xact_start timestamp with time zone, query_start timestamp with time zone, backend_start timestamp with time zone, state_change timestamp with time zone, client_addr inet, client_hostname text, client_port integer, backend_xid xid, backend_xmin xid) returns setof record;
create FUNCTION pg_stat_get_analyze_count($1 oid) returns bigint;
create FUNCTION pg_stat_get_archiver(archived_count bigint, last_archived_wal text, last_archived_time timestamp with time zone, failed_count bigint, last_failed_wal text, last_failed_time timestamp with time zone, stats_reset timestamp with time zone) returns record;
create FUNCTION pg_stat_get_autoanalyze_count($1 oid) returns bigint;
create FUNCTION pg_stat_get_autovacuum_count($1 oid) returns bigint;
create FUNCTION pg_stat_get_backend_activity_start($1 integer) returns timestamp with time zone;
create FUNCTION pg_stat_get_backend_activity($1 integer) returns text;
create FUNCTION pg_stat_get_backend_client_addr($1 integer) returns inet;
create FUNCTION pg_stat_get_backend_client_port($1 integer) returns integer;
create FUNCTION pg_stat_get_backend_dbid($1 integer) returns oid;
create FUNCTION pg_stat_get_backend_idset();
create FUNCTION pg_stat_get_backend_pid($1 integer) returns integer;
create FUNCTION pg_stat_get_backend_start($1 integer) returns timestamp with time zone;
create FUNCTION pg_stat_get_backend_userid($1 integer) returns oid;
create FUNCTION pg_stat_get_backend_waiting($1 integer) returns boolean;
create FUNCTION pg_stat_get_backend_xact_start($1 integer) returns timestamp with time zone;
create FUNCTION pg_stat_get_bgwriter_buf_written_checkpoints();
create FUNCTION pg_stat_get_bgwriter_buf_written_clean();
create FUNCTION pg_stat_get_bgwriter_maxwritten_clean();
create FUNCTION pg_stat_get_bgwriter_requested_checkpoints();
create FUNCTION pg_stat_get_bgwriter_stat_reset_time();
create FUNCTION pg_stat_get_bgwriter_timed_checkpoints();
create FUNCTION pg_stat_get_blocks_fetched($1 oid) returns bigint;
create FUNCTION pg_stat_get_blocks_hit($1 oid) returns bigint;
create FUNCTION pg_stat_get_buf_alloc();
create FUNCTION pg_stat_get_buf_fsync_backend();
create FUNCTION pg_stat_get_buf_written_backend();
create FUNCTION pg_stat_get_checkpoint_sync_time();
create FUNCTION pg_stat_get_checkpoint_write_time();
create FUNCTION pg_stat_get_db_blk_read_time($1 oid) returns double precision;
create FUNCTION pg_stat_get_db_blk_write_time($1 oid) returns double precision;
create FUNCTION pg_stat_get_db_blocks_fetched($1 oid) returns bigint;
create FUNCTION pg_stat_get_db_blocks_hit($1 oid) returns bigint;
create FUNCTION pg_stat_get_db_conflict_all($1 oid) returns bigint;
create FUNCTION pg_stat_get_db_conflict_bufferpin($1 oid) returns bigint;
create FUNCTION pg_stat_get_db_conflict_lock($1 oid) returns bigint;
create FUNCTION pg_stat_get_db_conflict_snapshot($1 oid) returns bigint;
create FUNCTION pg_stat_get_db_conflict_startup_deadlock($1 oid) returns bigint;
create FUNCTION pg_stat_get_db_conflict_tablespace($1 oid) returns bigint;
create FUNCTION pg_stat_get_db_deadlocks($1 oid) returns bigint;
create FUNCTION pg_stat_get_db_numbackends($1 oid) returns integer;
create FUNCTION pg_stat_get_db_stat_reset_time($1 oid) returns timestamp with time zone;
create FUNCTION pg_stat_get_db_temp_bytes($1 oid) returns bigint;
create FUNCTION pg_stat_get_db_temp_files($1 oid) returns bigint;
create FUNCTION pg_stat_get_db_tuples_deleted($1 oid) returns bigint;
create FUNCTION pg_stat_get_db_tuples_fetched($1 oid) returns bigint;
create FUNCTION pg_stat_get_db_tuples_inserted($1 oid) returns bigint;
create FUNCTION pg_stat_get_db_tuples_returned($1 oid) returns bigint;
create FUNCTION pg_stat_get_db_tuples_updated($1 oid) returns bigint;
create FUNCTION pg_stat_get_db_xact_commit($1 oid) returns bigint;
create FUNCTION pg_stat_get_db_xact_rollback($1 oid) returns bigint;
create FUNCTION pg_stat_get_dead_tuples($1 oid) returns bigint;
create FUNCTION pg_stat_get_function_calls($1 oid) returns bigint;
create FUNCTION pg_stat_get_function_self_time($1 oid) returns double precision;
create FUNCTION pg_stat_get_function_total_time($1 oid) returns double precision;
create FUNCTION pg_stat_get_last_analyze_time($1 oid) returns timestamp with time zone;
create FUNCTION pg_stat_get_last_autoanalyze_time($1 oid) returns timestamp with time zone;
create FUNCTION pg_stat_get_last_autovacuum_time($1 oid) returns timestamp with time zone;
create FUNCTION pg_stat_get_last_vacuum_time($1 oid) returns timestamp with time zone;
create FUNCTION pg_stat_get_live_tuples($1 oid) returns bigint;
create FUNCTION pg_stat_get_mod_since_analyze($1 oid) returns bigint;
create FUNCTION pg_stat_get_numscans($1 oid) returns bigint;
create FUNCTION pg_stat_get_tuples_deleted($1 oid) returns bigint;
create FUNCTION pg_stat_get_tuples_fetched($1 oid) returns bigint;
create FUNCTION pg_stat_get_tuples_hot_updated($1 oid) returns bigint;
create FUNCTION pg_stat_get_tuples_inserted($1 oid) returns bigint;
create FUNCTION pg_stat_get_tuples_returned($1 oid) returns bigint;
create FUNCTION pg_stat_get_tuples_updated($1 oid) returns bigint;
create FUNCTION pg_stat_get_vacuum_count($1 oid) returns bigint;
create FUNCTION pg_stat_get_wal_senders(pid integer, state text, sent_location pg_lsn, write_location pg_lsn, flush_location pg_lsn, replay_location pg_lsn, sync_priority integer, sync_state text) returns setof record;
create FUNCTION pg_stat_get_xact_blocks_fetched($1 oid) returns bigint;
create FUNCTION pg_stat_get_xact_blocks_hit($1 oid) returns bigint;
create FUNCTION pg_stat_get_xact_function_calls($1 oid) returns bigint;
create FUNCTION pg_stat_get_xact_function_self_time($1 oid) returns double precision;
create FUNCTION pg_stat_get_xact_function_total_time($1 oid) returns double precision;
create FUNCTION pg_stat_get_xact_numscans($1 oid) returns bigint;
create FUNCTION pg_stat_get_xact_tuples_deleted($1 oid) returns bigint;
create FUNCTION pg_stat_get_xact_tuples_fetched($1 oid) returns bigint;
create FUNCTION pg_stat_get_xact_tuples_hot_updated($1 oid) returns bigint;
create FUNCTION pg_stat_get_xact_tuples_inserted($1 oid) returns bigint;
create FUNCTION pg_stat_get_xact_tuples_returned($1 oid) returns bigint;
create FUNCTION pg_stat_get_xact_tuples_updated($1 oid) returns bigint;
create FUNCTION pg_stat_reset();
create FUNCTION pg_stat_reset_shared($1 text) returns void;
create FUNCTION pg_stat_reset_single_function_counters($1 oid) returns void;
create FUNCTION pg_stat_reset_single_table_counters($1 oid) returns void;
create FUNCTION pg_stop_backup();
create FUNCTION pg_switch_xlog();
create FUNCTION pg_table_is_visible($1 oid) returns boolean;
create FUNCTION pg_table_size($1 regclass) returns bigint;
create FUNCTION pg_tablespace_databases($1 oid) returns setof oid;
create FUNCTION pg_tablespace_location($1 oid) returns text;
create FUNCTION pg_tablespace_size($1 name) returns bigint;
create FUNCTION pg_tablespace_size($1 oid) returns bigint;
create FUNCTION pg_terminate_backend($1 integer) returns boolean;
create FUNCTION pg_timezone_abbrevs(abbrev text, utc_offset interval, is_dst boolean) returns setof record;
create FUNCTION pg_timezone_names(name text, abbrev text, utc_offset interval, is_dst boolean) returns setof record;
create FUNCTION pg_total_relation_size($1 regclass) returns bigint;
create FUNCTION pg_trigger_depth();
create FUNCTION pg_try_advisory_lock_shared($1 bigint) returns boolean;
create FUNCTION pg_try_advisory_lock_shared($1 integer, $2 integer) returns boolean;
create FUNCTION pg_try_advisory_lock($1 bigint) returns boolean;
create FUNCTION pg_try_advisory_lock($1 integer, $2 integer) returns boolean;
create FUNCTION pg_try_advisory_xact_lock_shared($1 bigint) returns boolean;
create FUNCTION pg_try_advisory_xact_lock_shared($1 integer, $2 integer) returns boolean;
create FUNCTION pg_try_advisory_xact_lock($1 bigint) returns boolean;
create FUNCTION pg_try_advisory_xact_lock($1 integer, $2 integer) returns boolean;
create FUNCTION pg_ts_config_is_visible($1 oid) returns boolean;
create FUNCTION pg_ts_dict_is_visible($1 oid) returns boolean;
create FUNCTION pg_ts_parser_is_visible($1 oid) returns boolean;
create FUNCTION pg_ts_template_is_visible($1 oid) returns boolean;
create FUNCTION pg_type_is_visible($1 oid) returns boolean;
create FUNCTION pg_typeof($1 "any") returns regtype;
create FUNCTION pg_xlog_location_diff($1 pg_lsn, $2 pg_lsn) returns numeric;
create FUNCTION pg_xlog_replay_pause();
create FUNCTION pg_xlog_replay_resume();
create FUNCTION pg_xlogfile_name_offset(wal_location pg_lsn, file_name text, file_offset integer) returns record;
create FUNCTION pg_xlogfile_name($1 pg_lsn) returns text;
create FUNCTION pi();
create FUNCTION plainto_tsquery($1 regconfig, $2 text) returns tsquery;
create FUNCTION plainto_tsquery($1 text) returns tsquery;
create FUNCTION plpgsql_call_handler();
create FUNCTION plpgsql_inline_handler($1 internal) returns void;
create FUNCTION plpgsql_validator($1 oid) returns void;
create FUNCTION point_above($1 point, $2 point) returns boolean;
create FUNCTION point_add($1 point, $2 point) returns point;
create FUNCTION point_below($1 point, $2 point) returns boolean;
create FUNCTION point_distance($1 point, $2 point) returns double precision;
create FUNCTION point_div($1 point, $2 point) returns point;
create FUNCTION point_eq($1 point, $2 point) returns boolean;
create FUNCTION point_horiz($1 point, $2 point) returns boolean;
create FUNCTION point_in($1 cstring) returns point;
create FUNCTION point_left($1 point, $2 point) returns boolean;
create FUNCTION point_mul($1 point, $2 point) returns point;
create FUNCTION point_ne($1 point, $2 point) returns boolean;
create FUNCTION point_out($1 point) returns cstring;
create FUNCTION point_recv($1 internal) returns point;
create FUNCTION point_right($1 point, $2 point) returns boolean;
create FUNCTION point_send($1 point) returns bytea;
create FUNCTION point_sub($1 point, $2 point) returns point;
create FUNCTION point_vert($1 point, $2 point) returns boolean;
create FUNCTION point($1 box) returns point;
create FUNCTION point($1 circle) returns point;
create FUNCTION point($1 double precision, $2 double precision) returns point;
create FUNCTION point($1 lseg) returns point;
create FUNCTION point($1 path) returns point;
create FUNCTION point($1 polygon) returns point;
create FUNCTION poly_above($1 polygon, $2 polygon) returns boolean;
create FUNCTION poly_below($1 polygon, $2 polygon) returns boolean;
create FUNCTION poly_center($1 polygon) returns point;
create FUNCTION poly_contain_pt($1 polygon, $2 point) returns boolean;
create FUNCTION poly_contained($1 polygon, $2 polygon) returns boolean;
create FUNCTION poly_contain($1 polygon, $2 polygon) returns boolean;
create FUNCTION poly_distance($1 polygon, $2 polygon) returns double precision;
create FUNCTION poly_in($1 cstring) returns polygon;
create FUNCTION poly_left($1 polygon, $2 polygon) returns boolean;
create FUNCTION poly_npoints($1 polygon) returns integer;
create FUNCTION poly_out($1 polygon) returns cstring;
create FUNCTION poly_overabove($1 polygon, $2 polygon) returns boolean;
create FUNCTION poly_overbelow($1 polygon, $2 polygon) returns boolean;
create FUNCTION poly_overlap($1 polygon, $2 polygon) returns boolean;
create FUNCTION poly_overleft($1 polygon, $2 polygon) returns boolean;
create FUNCTION poly_overright($1 polygon, $2 polygon) returns boolean;
create FUNCTION poly_recv($1 internal) returns polygon;
create FUNCTION poly_right($1 polygon, $2 polygon) returns boolean;
create FUNCTION poly_same($1 polygon, $2 polygon) returns boolean;
create FUNCTION poly_send($1 polygon) returns bytea;
create FUNCTION polygon($1 box) returns polygon;
create FUNCTION polygon($1 circle) returns polygon;
create FUNCTION polygon($1 integer, $2 circle) returns polygon;
create FUNCTION polygon($1 path) returns polygon;
create FUNCTION popen($1 path) returns path;
create FUNCTION "position"($1 bit, $2 bit) returns integer;
create FUNCTION "position"($1 bytea, $2 bytea) returns integer;
create FUNCTION positionjoinsel($1 internal, $2 oid, $3 internal, $4 smallint, $5 internal) returns double precision;
create FUNCTION positionsel($1 internal, $2 oid, $3 internal, $4 integer) returns double precision;
create FUNCTION "position"($1 text, $2 text) returns integer;
create FUNCTION postgresql_fdw_validator($1 text[], $2 oid) returns boolean;
create FUNCTION pow($1 double precision, $2 double precision) returns double precision;
create FUNCTION power($1 double precision, $2 double precision) returns double precision;
create FUNCTION power($1 numeric, $2 numeric) returns numeric;
create FUNCTION pow($1 numeric, $2 numeric) returns numeric;
create FUNCTION prsd_end($1 internal) returns void;
create FUNCTION prsd_headline($1 internal, $2 internal, $3 tsquery) returns internal;
create FUNCTION prsd_lextype($1 internal) returns internal;
create FUNCTION prsd_nexttoken($1 internal, $2 internal, $3 internal) returns internal;
create FUNCTION prsd_start($1 internal, $2 integer) returns internal;
create FUNCTION pt_contained_circle($1 point, $2 circle) returns boolean;
create FUNCTION pt_contained_poly($1 point, $2 polygon) returns boolean;
create FUNCTION query_to_xml_and_xmlschema(query text, nulls boolean, tableforest boolean, targetns text) returns xml;
create FUNCTION query_to_xmlschema(query text, nulls boolean, tableforest boolean, targetns text) returns xml;
create FUNCTION query_to_xml(query text, nulls boolean, tableforest boolean, targetns text) returns xml;
create FUNCTION querytree($1 tsquery) returns text;
create FUNCTION quote_ident($1 text) returns text;
create FUNCTION quote_literal($1 anyelement) returns text;
create FUNCTION quote_literal($1 text) returns text;
create FUNCTION quote_nullable($1 anyelement) returns text;
create FUNCTION quote_nullable($1 text) returns text;
create FUNCTION radians($1 double precision) returns double precision;
create FUNCTION radius($1 circle) returns double precision;
create FUNCTION random();
create FUNCTION range_adjacent($1 anyrange, $2 anyrange) returns boolean;
create FUNCTION range_after($1 anyrange, $2 anyrange) returns boolean;
create FUNCTION range_before($1 anyrange, $2 anyrange) returns boolean;
create FUNCTION range_cmp($1 anyrange, $2 anyrange) returns integer;
create FUNCTION range_contained_by($1 anyrange, $2 anyrange) returns boolean;
create FUNCTION range_contains_elem($1 anyrange, $2 anyelement) returns boolean;
create FUNCTION range_contains($1 anyrange, $2 anyrange) returns boolean;
create FUNCTION range_eq($1 anyrange, $2 anyrange) returns boolean;
create FUNCTION range_ge($1 anyrange, $2 anyrange) returns boolean;
create FUNCTION range_gist_compress($1 internal) returns internal;
create FUNCTION range_gist_consistent($1 internal, $2 anyrange, $3 integer, $4 oid, $5 internal) returns boolean;
create FUNCTION range_gist_decompress($1 internal) returns internal;
create FUNCTION range_gist_penalty($1 internal, $2 internal, $3 internal) returns internal;
create FUNCTION range_gist_picksplit($1 internal, $2 internal) returns internal;
create FUNCTION range_gist_same($1 anyrange, $2 anyrange, $3 internal) returns internal;
create FUNCTION range_gist_union($1 internal, $2 internal) returns internal;
create FUNCTION range_gt($1 anyrange, $2 anyrange) returns boolean;
create FUNCTION range_in($1 cstring, $2 oid, $3 integer) returns anyrange;
create FUNCTION range_intersect($1 anyrange, $2 anyrange) returns anyrange;
create FUNCTION range_le($1 anyrange, $2 anyrange) returns boolean;
create FUNCTION range_lt($1 anyrange, $2 anyrange) returns boolean;
create FUNCTION range_minus($1 anyrange, $2 anyrange) returns anyrange;
create FUNCTION range_ne($1 anyrange, $2 anyrange) returns boolean;
create FUNCTION range_out($1 anyrange) returns cstring;
create FUNCTION range_overlaps($1 anyrange, $2 anyrange) returns boolean;
create FUNCTION range_overleft($1 anyrange, $2 anyrange) returns boolean;
create FUNCTION range_overright($1 anyrange, $2 anyrange) returns boolean;
create FUNCTION range_recv($1 internal, $2 oid, $3 integer) returns anyrange;
create FUNCTION range_send($1 anyrange) returns bytea;
create FUNCTION range_typanalyze($1 internal) returns boolean;
create FUNCTION range_union($1 anyrange, $2 anyrange) returns anyrange;
create FUNCTION rangesel($1 internal, $2 oid, $3 internal, $4 integer) returns double precision;
create FUNCTION rank();
create FUNCTION rank($1 "any") returns bigint;
create FUNCTION rank_final($1 internal, $2 "any") returns bigint;
create FUNCTION record_eq($1 record, $2 record) returns boolean;
create FUNCTION record_ge($1 record, $2 record) returns boolean;
create FUNCTION record_gt($1 record, $2 record) returns boolean;
create FUNCTION record_image_eq($1 record, $2 record) returns boolean;
create FUNCTION record_image_ge($1 record, $2 record) returns boolean;
create FUNCTION record_image_gt($1 record, $2 record) returns boolean;
create FUNCTION record_image_le($1 record, $2 record) returns boolean;
create FUNCTION record_image_lt($1 record, $2 record) returns boolean;
create FUNCTION record_image_ne($1 record, $2 record) returns boolean;
create FUNCTION record_in($1 cstring, $2 oid, $3 integer) returns record;
create FUNCTION record_le($1 record, $2 record) returns boolean;
create FUNCTION record_lt($1 record, $2 record) returns boolean;
create FUNCTION record_ne($1 record, $2 record) returns boolean;
create FUNCTION record_out($1 record) returns cstring;
create FUNCTION record_recv($1 internal, $2 oid, $3 integer) returns record;
create FUNCTION record_send($1 record) returns bytea;
create FUNCTION regclassin($1 cstring) returns regclass;
create FUNCTION regclassout($1 regclass) returns cstring;
create FUNCTION regclassrecv($1 internal) returns regclass;
create FUNCTION regclasssend($1 regclass) returns bytea;
create FUNCTION regclass($1 text) returns regclass;
create FUNCTION regconfigin($1 cstring) returns regconfig;
create FUNCTION regconfigout($1 regconfig) returns cstring;
create FUNCTION regconfigrecv($1 internal) returns regconfig;
create FUNCTION regconfigsend($1 regconfig) returns bytea;
create FUNCTION regdictionaryin($1 cstring) returns regdictionary;
create FUNCTION regdictionaryout($1 regdictionary) returns cstring;
create FUNCTION regdictionaryrecv($1 internal) returns regdictionary;
create FUNCTION regdictionarysend($1 regdictionary) returns bytea;
create FUNCTION regexeqjoinsel($1 internal, $2 oid, $3 internal, $4 smallint, $5 internal) returns double precision;
create FUNCTION regexeqsel($1 internal, $2 oid, $3 internal, $4 integer) returns double precision;
create FUNCTION regexnejoinsel($1 internal, $2 oid, $3 internal, $4 smallint, $5 internal) returns double precision;
create FUNCTION regexnesel($1 internal, $2 oid, $3 internal, $4 integer) returns double precision;
create FUNCTION regexp_matches($1 text, $2 text) returns setof text[];
create FUNCTION regexp_matches($1 text, $2 text, $3 text) returns setof text[];
create FUNCTION regexp_replace($1 text, $2 text, $3 text) returns text;
create FUNCTION regexp_replace($1 text, $2 text, $3 text, $4 text) returns text;
create FUNCTION regexp_split_to_array($1 text, $2 text) returns text[];
create FUNCTION regexp_split_to_array($1 text, $2 text, $3 text) returns text[];
create FUNCTION regexp_split_to_table($1 text, $2 text) returns setof text;
create FUNCTION regexp_split_to_table($1 text, $2 text, $3 text) returns setof text;
create FUNCTION regoperatorin($1 cstring) returns regoperator;
create FUNCTION regoperatorout($1 regoperator) returns cstring;
create FUNCTION regoperatorrecv($1 internal) returns regoperator;
create FUNCTION regoperatorsend($1 regoperator) returns bytea;
create FUNCTION regoperin($1 cstring) returns regoper;
create FUNCTION regoperout($1 regoper) returns cstring;
create FUNCTION regoperrecv($1 internal) returns regoper;
create FUNCTION regopersend($1 regoper) returns bytea;
create FUNCTION regprocedurein($1 cstring) returns regprocedure;
create FUNCTION regprocedureout($1 regprocedure) returns cstring;
create FUNCTION regprocedurerecv($1 internal) returns regprocedure;
create FUNCTION regproceduresend($1 regprocedure) returns bytea;
create FUNCTION regprocin($1 cstring) returns regproc;
create FUNCTION regprocout($1 regproc) returns cstring;
create FUNCTION regprocrecv($1 internal) returns regproc;
create FUNCTION regprocsend($1 regproc) returns bytea;
create FUNCTION regr_avgx($1 double precision, $2 double precision) returns double precision;
create FUNCTION regr_avgy($1 double precision, $2 double precision) returns double precision;
create FUNCTION regr_count($1 double precision, $2 double precision) returns bigint;
create FUNCTION regr_intercept($1 double precision, $2 double precision) returns double precision;
create FUNCTION regr_r2($1 double precision, $2 double precision) returns double precision;
create FUNCTION regr_slope($1 double precision, $2 double precision) returns double precision;
create FUNCTION regr_sxx($1 double precision, $2 double precision) returns double precision;
create FUNCTION regr_sxy($1 double precision, $2 double precision) returns double precision;
create FUNCTION regr_syy($1 double precision, $2 double precision) returns double precision;
create FUNCTION regtypein($1 cstring) returns regtype;
create FUNCTION regtypeout($1 regtype) returns cstring;
create FUNCTION regtyperecv($1 internal) returns regtype;
create FUNCTION regtypesend($1 regtype) returns bytea;
create FUNCTION reltimeeq($1 reltime, $2 reltime) returns boolean;
create FUNCTION reltimege($1 reltime, $2 reltime) returns boolean;
create FUNCTION reltimegt($1 reltime, $2 reltime) returns boolean;
create FUNCTION reltimein($1 cstring) returns reltime;
create FUNCTION reltime($1 interval) returns reltime;
create FUNCTION reltimele($1 reltime, $2 reltime) returns boolean;
create FUNCTION reltimelt($1 reltime, $2 reltime) returns boolean;
create FUNCTION reltimene($1 reltime, $2 reltime) returns boolean;
create FUNCTION reltimeout($1 reltime) returns cstring;
create FUNCTION reltimerecv($1 internal) returns reltime;
create FUNCTION reltimesend($1 reltime) returns bytea;
create FUNCTION repeat($1 text, $2 integer) returns text;
create FUNCTION replace($1 text, $2 text, $3 text) returns text;
create FUNCTION reverse($1 text) returns text;
create FUNCTION "right"($1 text, $2 integer) returns text;
create FUNCTION round($1 double precision) returns double precision;
create FUNCTION round($1 numeric) returns numeric;
create FUNCTION round($1 numeric, $2 integer) returns numeric;
create FUNCTION row_number();
create FUNCTION row_to_json($1 record) returns json;
create FUNCTION row_to_json($1 record, $2 boolean) returns json;
create FUNCTION rpad($1 text, $2 integer) returns text;
create FUNCTION rpad($1 text, $2 integer, $3 text) returns text;
create FUNCTION rtrim($1 text) returns text;
create FUNCTION rtrim($1 text, $2 text) returns text;
create FUNCTION scalargtjoinsel($1 internal, $2 oid, $3 internal, $4 smallint, $5 internal) returns double precision;
create FUNCTION scalargtsel($1 internal, $2 oid, $3 internal, $4 integer) returns double precision;
create FUNCTION scalarltjoinsel($1 internal, $2 oid, $3 internal, $4 smallint, $5 internal) returns double precision;
create FUNCTION scalarltsel($1 internal, $2 oid, $3 internal, $4 integer) returns double precision;
create FUNCTION schema_to_xml_and_xmlschema("schema" name, nulls boolean, tableforest boolean, targetns text) returns xml;
create FUNCTION schema_to_xml("schema" name, nulls boolean, tableforest boolean, targetns text) returns xml;
create FUNCTION schema_to_xmlschema("schema" name, nulls boolean, tableforest boolean, targetns text) returns xml;
create FUNCTION "session_user"();
create FUNCTION set_bit($1 bit, $2 integer, $3 integer) returns bit;
create FUNCTION set_bit($1 bytea, $2 integer, $3 integer) returns bytea;
create FUNCTION set_byte($1 bytea, $2 integer, $3 integer) returns bytea;
create FUNCTION set_config($1 text, $2 text, $3 boolean) returns text;
create FUNCTION set_masklen($1 cidr, $2 integer) returns cidr;
create FUNCTION set_masklen($1 inet, $2 integer) returns inet;
create FUNCTION setseed($1 double precision) returns void;
create FUNCTION setval($1 regclass, $2 bigint) returns bigint;
create FUNCTION setval($1 regclass, $2 bigint, $3 boolean) returns bigint;
create FUNCTION setweight($1 tsvector, $2 "char") returns tsvector;
create FUNCTION shell_in($1 cstring) returns opaque;
create FUNCTION shell_out($1 opaque) returns cstring;
create FUNCTION shift_jis_2004_to_euc_jis_2004($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION shift_jis_2004_to_utf8($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION shobj_description($1 oid, $2 name) returns text;
create FUNCTION sign($1 double precision) returns double precision;
create FUNCTION sign($1 numeric) returns numeric;
create FUNCTION similar_escape($1 text, $2 text) returns text;
create FUNCTION sin($1 double precision) returns double precision;
create FUNCTION sjis_to_euc_jp($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION sjis_to_mic($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION sjis_to_utf8($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION slope($1 point, $2 point) returns double precision;
create FUNCTION smgreq($1 smgr, $2 smgr) returns boolean;
create FUNCTION smgrin($1 cstring) returns smgr;
create FUNCTION smgrne($1 smgr, $2 smgr) returns boolean;
create FUNCTION smgrout($1 smgr) returns cstring;
create FUNCTION spg_kd_choose($1 internal, $2 internal) returns void;
create FUNCTION spg_kd_config($1 internal, $2 internal) returns void;
create FUNCTION spg_kd_inner_consistent($1 internal, $2 internal) returns void;
create FUNCTION spg_kd_picksplit($1 internal, $2 internal) returns void;
create FUNCTION spg_quad_choose($1 internal, $2 internal) returns void;
create FUNCTION spg_quad_config($1 internal, $2 internal) returns void;
create FUNCTION spg_quad_inner_consistent($1 internal, $2 internal) returns void;
create FUNCTION spg_quad_leaf_consistent($1 internal, $2 internal) returns boolean;
create FUNCTION spg_quad_picksplit($1 internal, $2 internal) returns void;
create FUNCTION spg_range_quad_choose($1 internal, $2 internal) returns void;
create FUNCTION spg_range_quad_config($1 internal, $2 internal) returns void;
create FUNCTION spg_range_quad_inner_consistent($1 internal, $2 internal) returns void;
create FUNCTION spg_range_quad_leaf_consistent($1 internal, $2 internal) returns boolean;
create FUNCTION spg_range_quad_picksplit($1 internal, $2 internal) returns void;
create FUNCTION spg_text_choose($1 internal, $2 internal) returns void;
create FUNCTION spg_text_config($1 internal, $2 internal) returns void;
create FUNCTION spg_text_inner_consistent($1 internal, $2 internal) returns void;
create FUNCTION spg_text_leaf_consistent($1 internal, $2 internal) returns boolean;
create FUNCTION spg_text_picksplit($1 internal, $2 internal) returns void;
create FUNCTION spgbeginscan($1 internal, $2 internal, $3 internal) returns internal;
create FUNCTION spgbuildempty($1 internal) returns void;
create FUNCTION spgbuild($1 internal, $2 internal, $3 internal) returns internal;
create FUNCTION spgbulkdelete($1 internal, $2 internal, $3 internal, $4 internal) returns internal;
create FUNCTION spgcanreturn($1 internal) returns boolean;
create FUNCTION spgcostestimate($1 internal, $2 internal, $3 internal, $4 internal, $5 internal, $6 internal, $7 internal) returns void;
create FUNCTION spgendscan($1 internal) returns void;
create FUNCTION spggetbitmap($1 internal, $2 internal) returns bigint;
create FUNCTION spggettuple($1 internal, $2 internal) returns boolean;
create FUNCTION spginsert($1 internal, $2 internal, $3 internal, $4 internal, $5 internal, $6 internal) returns boolean;
create FUNCTION spgmarkpos($1 internal) returns void;
create FUNCTION spgoptions($1 text[], $2 boolean) returns bytea;
create FUNCTION spgrescan($1 internal, $2 internal, $3 internal, $4 internal, $5 internal) returns void;
create FUNCTION spgrestrpos($1 internal) returns void;
create FUNCTION spgvacuumcleanup($1 internal, $2 internal) returns internal;
create FUNCTION split_part($1 text, $2 text, $3 integer) returns text;
create FUNCTION sqrt($1 double precision) returns double precision;
create FUNCTION sqrt($1 numeric) returns numeric;
create FUNCTION statement_timestamp();
create FUNCTION stddev_pop($1 bigint) returns numeric;
create FUNCTION stddev_pop($1 double precision) returns double precision;
create FUNCTION stddev_pop($1 integer) returns numeric;
create FUNCTION stddev_pop($1 numeric) returns numeric;
create FUNCTION stddev_pop($1 real) returns double precision;
create FUNCTION stddev_pop($1 smallint) returns numeric;
create FUNCTION stddev_samp($1 bigint) returns numeric;
create FUNCTION stddev_samp($1 double precision) returns double precision;
create FUNCTION stddev_samp($1 integer) returns numeric;
create FUNCTION stddev_samp($1 numeric) returns numeric;
create FUNCTION stddev_samp($1 real) returns double precision;
create FUNCTION stddev_samp($1 smallint) returns numeric;
create FUNCTION stddev($1 bigint) returns numeric;
create FUNCTION stddev($1 double precision) returns double precision;
create FUNCTION stddev($1 integer) returns numeric;
create FUNCTION stddev($1 numeric) returns numeric;
create FUNCTION stddev($1 real) returns double precision;
create FUNCTION stddev($1 smallint) returns numeric;
create FUNCTION string_agg_finalfn($1 internal) returns text;
create FUNCTION string_agg_transfn($1 internal, $2 text, $3 text) returns internal;
create FUNCTION string_agg($1 bytea, $2 bytea) returns bytea;
create FUNCTION string_agg($1 text, $2 text) returns text;
create FUNCTION string_to_array($1 text, $2 text) returns text[];
create FUNCTION string_to_array($1 text, $2 text, $3 text) returns text[];
create FUNCTION strip($1 tsvector) returns tsvector;
create FUNCTION strpos($1 text, $2 text) returns integer;
create FUNCTION substr($1 bytea, $2 integer) returns bytea;
create FUNCTION substr($1 bytea, $2 integer, $3 integer) returns bytea;
create FUNCTION "substring"($1 bit, $2 integer) returns bit;
create FUNCTION "substring"($1 bit, $2 integer, $3 integer) returns bit;
create FUNCTION "substring"($1 bytea, $2 integer) returns bytea;
create FUNCTION "substring"($1 bytea, $2 integer, $3 integer) returns bytea;
create FUNCTION "substring"($1 text, $2 integer) returns text;
create FUNCTION "substring"($1 text, $2 integer, $3 integer) returns text;
create FUNCTION "substring"($1 text, $2 text) returns text;
create FUNCTION "substring"($1 text, $2 text, $3 text) returns text;
create FUNCTION substr($1 text, $2 integer) returns text;
create FUNCTION substr($1 text, $2 integer, $3 integer) returns text;
create FUNCTION "sum"($1 bigint) returns numeric;
create FUNCTION "sum"($1 double precision) returns double precision;
create FUNCTION "sum"($1 integer) returns bigint;
create FUNCTION "sum"($1 interval) returns interval;
create FUNCTION "sum"($1 money) returns money;
create FUNCTION "sum"($1 numeric) returns numeric;
create FUNCTION "sum"($1 real) returns real;
create FUNCTION "sum"($1 smallint) returns bigint;
create FUNCTION suppress_redundant_updates_trigger();
create FUNCTION table_to_xml_and_xmlschema(tbl regclass, nulls boolean, tableforest boolean, targetns text) returns xml;
create FUNCTION table_to_xml(tbl regclass, nulls boolean, tableforest boolean, targetns text) returns xml;
create FUNCTION table_to_xmlschema(tbl regclass, nulls boolean, tableforest boolean, targetns text) returns xml;
create FUNCTION tan($1 double precision) returns double precision;
create FUNCTION text($1 "char") returns text;
create FUNCTION text_ge($1 text, $2 text) returns boolean;
create FUNCTION text_gt($1 text, $2 text) returns boolean;
create FUNCTION text_larger($1 text, $2 text) returns text;
create FUNCTION text_le($1 text, $2 text) returns boolean;
create FUNCTION text_lt($1 text, $2 text) returns boolean;
create FUNCTION text_pattern_ge($1 text, $2 text) returns boolean;
create FUNCTION text_pattern_gt($1 text, $2 text) returns boolean;
create FUNCTION text_pattern_le($1 text, $2 text) returns boolean;
create FUNCTION text_pattern_lt($1 text, $2 text) returns boolean;
create FUNCTION text_smaller($1 text, $2 text) returns text;
create FUNCTION textanycat($1 text, $2 anynonarray) returns text;
create FUNCTION text($1 boolean) returns text;
create FUNCTION textcat($1 text, $2 text) returns text;
create FUNCTION text($1 char) returns text;
create FUNCTION texteq($1 text, $2 text) returns boolean;
create FUNCTION texticlike($1 text, $2 text) returns boolean;
create FUNCTION texticnlike($1 text, $2 text) returns boolean;
create FUNCTION texticregexeq($1 text, $2 text) returns boolean;
create FUNCTION texticregexne($1 text, $2 text) returns boolean;
create FUNCTION textin($1 cstring) returns text;
create FUNCTION text($1 inet) returns text;
create FUNCTION textlen($1 text) returns integer;
create FUNCTION textlike($1 text, $2 text) returns boolean;
create FUNCTION text($1 name) returns text;
create FUNCTION textne($1 text, $2 text) returns boolean;
create FUNCTION textnlike($1 text, $2 text) returns boolean;
create FUNCTION textout($1 text) returns cstring;
create FUNCTION textrecv($1 internal) returns text;
create FUNCTION textregexeq($1 text, $2 text) returns boolean;
create FUNCTION textregexne($1 text, $2 text) returns boolean;
create FUNCTION textsend($1 text) returns bytea;
create FUNCTION text($1 xml) returns text;
create FUNCTION thesaurus_init($1 internal) returns internal;
create FUNCTION thesaurus_lexize($1 internal, $2 internal, $3 internal, $4 internal) returns internal;
create FUNCTION tideq($1 tid, $2 tid) returns boolean;
create FUNCTION tidge($1 tid, $2 tid) returns boolean;
create FUNCTION tidgt($1 tid, $2 tid) returns boolean;
create FUNCTION tidin($1 cstring) returns tid;
create FUNCTION tidlarger($1 tid, $2 tid) returns tid;
create FUNCTION tidle($1 tid, $2 tid) returns boolean;
create FUNCTION tidlt($1 tid, $2 tid) returns boolean;
create FUNCTION tidne($1 tid, $2 tid) returns boolean;
create FUNCTION tidout($1 tid) returns cstring;
create FUNCTION tidrecv($1 internal) returns tid;
create FUNCTION tidsend($1 tid) returns bytea;
create FUNCTION tidsmaller($1 tid, $2 tid) returns tid;
create FUNCTION time_cmp($1 time, $2 time) returns integer;
create FUNCTION time_eq($1 time, $2 time) returns boolean;
create FUNCTION time_ge($1 time, $2 time) returns boolean;
create FUNCTION time_gt($1 time, $2 time) returns boolean;
create FUNCTION time_hash($1 time) returns integer;
create FUNCTION time_in($1 cstring, $2 oid, $3 integer) returns time;
create FUNCTION time_larger($1 time, $2 time) returns time;
create FUNCTION time_le($1 time, $2 time) returns boolean;
create FUNCTION time_lt($1 time, $2 time) returns boolean;
create FUNCTION time_mi_interval($1 time, $2 interval) returns time;
create FUNCTION time_mi_time($1 time, $2 time) returns interval;
create FUNCTION time_ne($1 time, $2 time) returns boolean;
create FUNCTION time_out($1 time) returns cstring;
create FUNCTION time_pl_interval($1 time, $2 interval) returns time;
create FUNCTION time_recv($1 internal, $2 oid, $3 integer) returns time;
create FUNCTION time_send($1 time) returns bytea;
create FUNCTION time_smaller($1 time, $2 time) returns time;
create FUNCTION time_transform($1 internal) returns internal;
create FUNCTION "time"($1 abstime) returns time;
create FUNCTION timedate_pl($1 time, $2 date) returns timestamp;
create FUNCTION "time"($1 interval) returns time;
create FUNCTION timemi($1 abstime, $2 reltime) returns abstime;
create FUNCTION timenow();
create FUNCTION timeofday();
create FUNCTION timepl($1 abstime, $2 reltime) returns abstime;
create FUNCTION timestamp_cmp_date($1 timestamp, $2 date) returns integer;
create FUNCTION timestamp_cmp_timestamptz($1 timestamp, $2 timestamp with time zone) returns integer;
create FUNCTION timestamp_cmp($1 timestamp, $2 timestamp) returns integer;
create FUNCTION timestamp_eq_date($1 timestamp, $2 date) returns boolean;
create FUNCTION timestamp_eq_timestamptz($1 timestamp, $2 timestamp with time zone) returns boolean;
create FUNCTION timestamp_eq($1 timestamp, $2 timestamp) returns boolean;
create FUNCTION timestamp_ge_date($1 timestamp, $2 date) returns boolean;
create FUNCTION timestamp_ge_timestamptz($1 timestamp, $2 timestamp with time zone) returns boolean;
create FUNCTION timestamp_ge($1 timestamp, $2 timestamp) returns boolean;
create FUNCTION timestamp_gt_date($1 timestamp, $2 date) returns boolean;
create FUNCTION timestamp_gt_timestamptz($1 timestamp, $2 timestamp with time zone) returns boolean;
create FUNCTION timestamp_gt($1 timestamp, $2 timestamp) returns boolean;
create FUNCTION timestamp_hash($1 timestamp) returns integer;
create FUNCTION timestamp_in($1 cstring, $2 oid, $3 integer) returns timestamp;
create FUNCTION timestamp_larger($1 timestamp, $2 timestamp) returns timestamp;
create FUNCTION timestamp_le_date($1 timestamp, $2 date) returns boolean;
create FUNCTION timestamp_le_timestamptz($1 timestamp, $2 timestamp with time zone) returns boolean;
create FUNCTION timestamp_le($1 timestamp, $2 timestamp) returns boolean;
create FUNCTION timestamp_lt_date($1 timestamp, $2 date) returns boolean;
create FUNCTION timestamp_lt_timestamptz($1 timestamp, $2 timestamp with time zone) returns boolean;
create FUNCTION timestamp_lt($1 timestamp, $2 timestamp) returns boolean;
create FUNCTION timestamp_mi_interval($1 timestamp, $2 interval) returns timestamp;
create FUNCTION timestamp_mi($1 timestamp, $2 timestamp) returns interval;
create FUNCTION timestamp_ne_date($1 timestamp, $2 date) returns boolean;
create FUNCTION timestamp_ne_timestamptz($1 timestamp, $2 timestamp with time zone) returns boolean;
create FUNCTION timestamp_ne($1 timestamp, $2 timestamp) returns boolean;
create FUNCTION timestamp_out($1 timestamp) returns cstring;
create FUNCTION timestamp_pl_interval($1 timestamp, $2 interval) returns timestamp;
create FUNCTION timestamp_recv($1 internal, $2 oid, $3 integer) returns timestamp;
create FUNCTION timestamp_send($1 timestamp) returns bytea;
create FUNCTION timestamp_smaller($1 timestamp, $2 timestamp) returns timestamp;
create FUNCTION timestamp_sortsupport($1 internal) returns void;
create FUNCTION timestamp_transform($1 internal) returns internal;
create FUNCTION "timestamp"($1 abstime) returns timestamp;
create FUNCTION "timestamp"($1 date) returns timestamp;
create FUNCTION "timestamp"($1 date, $2 time) returns timestamp;
create FUNCTION "timestamp"($1 timestamp with time zone) returns timestamp;
create FUNCTION "timestamp"($1 timestamp, $2 integer) returns timestamp;
create FUNCTION timestamptypmodin($1 cstring[]) returns integer;
create FUNCTION timestamptypmodout($1 integer) returns cstring;
create FUNCTION timestamptz_cmp_date($1 timestamp with time zone, $2 date) returns integer;
create FUNCTION timestamptz_cmp_timestamp($1 timestamp with time zone, $2 timestamp) returns integer;
create FUNCTION timestamptz_cmp($1 timestamp with time zone, $2 timestamp with time zone) returns integer;
create FUNCTION timestamptz_eq_date($1 timestamp with time zone, $2 date) returns boolean;
create FUNCTION timestamptz_eq_timestamp($1 timestamp with time zone, $2 timestamp) returns boolean;
create FUNCTION timestamptz_eq($1 timestamp with time zone, $2 timestamp with time zone) returns boolean;
create FUNCTION timestamptz_ge_date($1 timestamp with time zone, $2 date) returns boolean;
create FUNCTION timestamptz_ge_timestamp($1 timestamp with time zone, $2 timestamp) returns boolean;
create FUNCTION timestamptz_ge($1 timestamp with time zone, $2 timestamp with time zone) returns boolean;
create FUNCTION timestamptz_gt_date($1 timestamp with time zone, $2 date) returns boolean;
create FUNCTION timestamptz_gt_timestamp($1 timestamp with time zone, $2 timestamp) returns boolean;
create FUNCTION timestamptz_gt($1 timestamp with time zone, $2 timestamp with time zone) returns boolean;
create FUNCTION timestamptz_in($1 cstring, $2 oid, $3 integer) returns timestamp with time zone;
create FUNCTION timestamptz_larger($1 timestamp with time zone, $2 timestamp with time zone) returns timestamp with time zone;
create FUNCTION timestamptz_le_date($1 timestamp with time zone, $2 date) returns boolean;
create FUNCTION timestamptz_le_timestamp($1 timestamp with time zone, $2 timestamp) returns boolean;
create FUNCTION timestamptz_le($1 timestamp with time zone, $2 timestamp with time zone) returns boolean;
create FUNCTION timestamptz_lt_date($1 timestamp with time zone, $2 date) returns boolean;
create FUNCTION timestamptz_lt_timestamp($1 timestamp with time zone, $2 timestamp) returns boolean;
create FUNCTION timestamptz_lt($1 timestamp with time zone, $2 timestamp with time zone) returns boolean;
create FUNCTION timestamptz_mi_interval($1 timestamp with time zone, $2 interval) returns timestamp with time zone;
create FUNCTION timestamptz_mi($1 timestamp with time zone, $2 timestamp with time zone) returns interval;
create FUNCTION timestamptz_ne_date($1 timestamp with time zone, $2 date) returns boolean;
create FUNCTION timestamptz_ne_timestamp($1 timestamp with time zone, $2 timestamp) returns boolean;
create FUNCTION timestamptz_ne($1 timestamp with time zone, $2 timestamp with time zone) returns boolean;
create FUNCTION timestamptz_out($1 timestamp with time zone) returns cstring;
create FUNCTION timestamptz_pl_interval($1 timestamp with time zone, $2 interval) returns timestamp with time zone;
create FUNCTION timestamptz_recv($1 internal, $2 oid, $3 integer) returns timestamp with time zone;
create FUNCTION timestamptz_send($1 timestamp with time zone) returns bytea;
create FUNCTION timestamptz_smaller($1 timestamp with time zone, $2 timestamp with time zone) returns timestamp with time zone;
create FUNCTION timestamptz($1 abstime) returns timestamp with time zone;
create FUNCTION timestamptz($1 date) returns timestamp with time zone;
create FUNCTION timestamptz($1 date, $2 time) returns timestamp with time zone;
create FUNCTION timestamptz($1 date, $2 time with time zone) returns timestamp with time zone;
create FUNCTION timestamptz($1 timestamp) returns timestamp with time zone;
create FUNCTION timestamptz($1 timestamp with time zone, $2 integer) returns timestamp with time zone;
create FUNCTION timestamptztypmodin($1 cstring[]) returns integer;
create FUNCTION timestamptztypmodout($1 integer) returns cstring;
create FUNCTION "time"($1 time with time zone) returns time;
create FUNCTION "time"($1 time, $2 integer) returns time;
create FUNCTION "time"($1 timestamp) returns time;
create FUNCTION "time"($1 timestamp with time zone) returns time;
create FUNCTION timetypmodin($1 cstring[]) returns integer;
create FUNCTION timetypmodout($1 integer) returns cstring;
create FUNCTION timetz_cmp($1 time with time zone, $2 time with time zone) returns integer;
create FUNCTION timetz_eq($1 time with time zone, $2 time with time zone) returns boolean;
create FUNCTION timetz_ge($1 time with time zone, $2 time with time zone) returns boolean;
create FUNCTION timetz_gt($1 time with time zone, $2 time with time zone) returns boolean;
create FUNCTION timetz_hash($1 time with time zone) returns integer;
create FUNCTION timetz_in($1 cstring, $2 oid, $3 integer) returns time with time zone;
create FUNCTION timetz_larger($1 time with time zone, $2 time with time zone) returns time with time zone;
create FUNCTION timetz_le($1 time with time zone, $2 time with time zone) returns boolean;
create FUNCTION timetz_lt($1 time with time zone, $2 time with time zone) returns boolean;
create FUNCTION timetz_mi_interval($1 time with time zone, $2 interval) returns time with time zone;
create FUNCTION timetz_ne($1 time with time zone, $2 time with time zone) returns boolean;
create FUNCTION timetz_out($1 time with time zone) returns cstring;
create FUNCTION timetz_pl_interval($1 time with time zone, $2 interval) returns time with time zone;
create FUNCTION timetz_recv($1 internal, $2 oid, $3 integer) returns time with time zone;
create FUNCTION timetz_send($1 time with time zone) returns bytea;
create FUNCTION timetz_smaller($1 time with time zone, $2 time with time zone) returns time with time zone;
create FUNCTION timetzdate_pl($1 time with time zone, $2 date) returns timestamp with time zone;
create FUNCTION timetz($1 time) returns time with time zone;
create FUNCTION timetz($1 time with time zone, $2 integer) returns time with time zone;
create FUNCTION timetz($1 timestamp with time zone) returns time with time zone;
create FUNCTION timetztypmodin($1 cstring[]) returns integer;
create FUNCTION timetztypmodout($1 integer) returns cstring;
create FUNCTION timezone($1 interval, $2 time with time zone) returns time with time zone;
create FUNCTION timezone($1 interval, $2 timestamp) returns timestamp with time zone;
create FUNCTION timezone($1 interval, $2 timestamp with time zone) returns timestamp;
create FUNCTION timezone($1 text, $2 time with time zone) returns time with time zone;
create FUNCTION timezone($1 text, $2 timestamp) returns timestamp with time zone;
create FUNCTION timezone($1 text, $2 timestamp with time zone) returns timestamp;
create FUNCTION tinterval($1 abstime, $2 abstime) returns tinterval;
create FUNCTION tintervalct($1 tinterval, $2 tinterval) returns boolean;
create FUNCTION tintervalend($1 tinterval) returns abstime;
create FUNCTION tintervaleq($1 tinterval, $2 tinterval) returns boolean;
create FUNCTION tintervalge($1 tinterval, $2 tinterval) returns boolean;
create FUNCTION tintervalgt($1 tinterval, $2 tinterval) returns boolean;
create FUNCTION tintervalin($1 cstring) returns tinterval;
create FUNCTION tintervalleneq($1 tinterval, $2 reltime) returns boolean;
create FUNCTION tintervallenge($1 tinterval, $2 reltime) returns boolean;
create FUNCTION tintervallengt($1 tinterval, $2 reltime) returns boolean;
create FUNCTION tintervallenle($1 tinterval, $2 reltime) returns boolean;
create FUNCTION tintervallenlt($1 tinterval, $2 reltime) returns boolean;
create FUNCTION tintervallenne($1 tinterval, $2 reltime) returns boolean;
create FUNCTION tintervalle($1 tinterval, $2 tinterval) returns boolean;
create FUNCTION tintervallt($1 tinterval, $2 tinterval) returns boolean;
create FUNCTION tintervalne($1 tinterval, $2 tinterval) returns boolean;
create FUNCTION tintervalout($1 tinterval) returns cstring;
create FUNCTION tintervalov($1 tinterval, $2 tinterval) returns boolean;
create FUNCTION tintervalrecv($1 internal) returns tinterval;
create FUNCTION tintervalrel($1 tinterval) returns reltime;
create FUNCTION tintervalsame($1 tinterval, $2 tinterval) returns boolean;
create FUNCTION tintervalsend($1 tinterval) returns bytea;
create FUNCTION tintervalstart($1 tinterval) returns abstime;
create FUNCTION to_ascii($1 text) returns text;
create FUNCTION to_ascii($1 text, $2 integer) returns text;
create FUNCTION to_ascii($1 text, $2 name) returns text;
create FUNCTION to_char($1 bigint, $2 text) returns text;
create FUNCTION to_char($1 double precision, $2 text) returns text;
create FUNCTION to_char($1 integer, $2 text) returns text;
create FUNCTION to_char($1 interval, $2 text) returns text;
create FUNCTION to_char($1 numeric, $2 text) returns text;
create FUNCTION to_char($1 real, $2 text) returns text;
create FUNCTION to_char($1 timestamp with time zone, $2 text) returns text;
create FUNCTION to_char($1 timestamp, $2 text) returns text;
create FUNCTION to_date($1 text, $2 text) returns date;
create FUNCTION to_hex($1 bigint) returns text;
create FUNCTION to_hex($1 integer) returns text;
create FUNCTION to_json($1 anyelement) returns json;
create FUNCTION to_number($1 text, $2 text) returns numeric;
create FUNCTION to_regclass($1 cstring) returns regclass;
create FUNCTION to_regoperator($1 cstring) returns regoperator;
create FUNCTION to_regoper($1 cstring) returns regoper;
create FUNCTION to_regproc($1 cstring) returns regproc;
create FUNCTION to_regprocedure($1 cstring) returns regprocedure;
create FUNCTION to_regtype($1 cstring) returns regtype;
create FUNCTION to_timestamp($1 double precision) returns timestamp with time zone;
create FUNCTION to_timestamp($1 text, $2 text) returns timestamp with time zone;
create FUNCTION to_tsquery($1 regconfig, $2 text) returns tsquery;
create FUNCTION to_tsquery($1 text) returns tsquery;
create FUNCTION to_tsvector($1 regconfig, $2 text) returns tsvector;
create FUNCTION to_tsvector($1 text) returns tsvector;
create FUNCTION transaction_timestamp();
create FUNCTION "translate"($1 text, $2 text, $3 text) returns text;
create FUNCTION trigger_in($1 cstring) returns trigger;
create FUNCTION trigger_out($1 trigger) returns cstring;
create FUNCTION trunc($1 double precision) returns double precision;
create FUNCTION trunc($1 macaddr) returns macaddr;
create FUNCTION trunc($1 numeric) returns numeric;
create FUNCTION trunc($1 numeric, $2 integer) returns numeric;
create FUNCTION ts_debug(config regconfig, document text, alias text, description text, token text, dictionaries regdictionary[], dictionary regdictionary, lexemes text[]) returns setof record;
create FUNCTION ts_debug(document text, alias text, description text, token text, dictionaries regdictionary[], dictionary regdictionary, lexemes text[]) returns setof record;
create FUNCTION ts_headline($1 regconfig, $2 text, $3 tsquery) returns text;
create FUNCTION ts_headline($1 regconfig, $2 text, $3 tsquery, $4 text) returns text;
create FUNCTION ts_headline($1 text, $2 tsquery) returns text;
create FUNCTION ts_headline($1 text, $2 tsquery, $3 text) returns text;
create FUNCTION ts_lexize($1 regdictionary, $2 text) returns text[];
create FUNCTION ts_match_qv($1 tsquery, $2 tsvector) returns boolean;
create FUNCTION ts_match_tq($1 text, $2 tsquery) returns boolean;
create FUNCTION ts_match_tt($1 text, $2 text) returns boolean;
create FUNCTION ts_match_vq($1 tsvector, $2 tsquery) returns boolean;
create FUNCTION ts_parse(parser_oid oid, txt text, tokid integer, token text) returns setof record;
create FUNCTION ts_parse(parser_name text, txt text, tokid integer, token text) returns setof record;
create FUNCTION ts_rank_cd($1 real[], $2 tsvector, $3 tsquery) returns real;
create FUNCTION ts_rank_cd($1 real[], $2 tsvector, $3 tsquery, $4 integer) returns real;
create FUNCTION ts_rank_cd($1 tsvector, $2 tsquery) returns real;
create FUNCTION ts_rank_cd($1 tsvector, $2 tsquery, $3 integer) returns real;
create FUNCTION ts_rank($1 real[], $2 tsvector, $3 tsquery) returns real;
create FUNCTION ts_rank($1 real[], $2 tsvector, $3 tsquery, $4 integer) returns real;
create FUNCTION ts_rank($1 tsvector, $2 tsquery) returns real;
create FUNCTION ts_rank($1 tsvector, $2 tsquery, $3 integer) returns real;
create FUNCTION ts_rewrite($1 tsquery, $2 text) returns tsquery;
create FUNCTION ts_rewrite($1 tsquery, $2 tsquery, $3 tsquery) returns tsquery;
create FUNCTION ts_stat(query text, word text, ndoc integer, nentry integer) returns setof record;
create FUNCTION ts_stat(query text, weights text, word text, ndoc integer, nentry integer) returns setof record;
create FUNCTION ts_token_type(parser_oid oid, tokid integer, alias text, description text) returns setof record;
create FUNCTION ts_token_type(parser_name text, tokid integer, alias text, description text) returns setof record;
create FUNCTION ts_typanalyze($1 internal) returns boolean;
create FUNCTION tsmatchjoinsel($1 internal, $2 oid, $3 internal, $4 smallint, $5 internal) returns double precision;
create FUNCTION tsmatchsel($1 internal, $2 oid, $3 internal, $4 integer) returns double precision;
create FUNCTION tsq_mcontained($1 tsquery, $2 tsquery) returns boolean;
create FUNCTION tsq_mcontains($1 tsquery, $2 tsquery) returns boolean;
create FUNCTION tsquery_and($1 tsquery, $2 tsquery) returns tsquery;
create FUNCTION tsquery_cmp($1 tsquery, $2 tsquery) returns integer;
create FUNCTION tsquery_eq($1 tsquery, $2 tsquery) returns boolean;
create FUNCTION tsquery_ge($1 tsquery, $2 tsquery) returns boolean;
create FUNCTION tsquery_gt($1 tsquery, $2 tsquery) returns boolean;
create FUNCTION tsquery_le($1 tsquery, $2 tsquery) returns boolean;
create FUNCTION tsquery_lt($1 tsquery, $2 tsquery) returns boolean;
create FUNCTION tsquery_ne($1 tsquery, $2 tsquery) returns boolean;
create FUNCTION tsquery_not($1 tsquery) returns tsquery;
create FUNCTION tsquery_or($1 tsquery, $2 tsquery) returns tsquery;
create FUNCTION tsqueryin($1 cstring) returns tsquery;
create FUNCTION tsqueryout($1 tsquery) returns cstring;
create FUNCTION tsqueryrecv($1 internal) returns tsquery;
create FUNCTION tsquerysend($1 tsquery) returns bytea;
create FUNCTION tsrange_subdiff($1 timestamp, $2 timestamp) returns double precision;
create FUNCTION tsrange($1 timestamp, $2 timestamp) returns tsrange;
create FUNCTION tsrange($1 timestamp, $2 timestamp, $3 text) returns tsrange;
create FUNCTION tstzrange_subdiff($1 timestamp with time zone, $2 timestamp with time zone) returns double precision;
create FUNCTION tstzrange($1 timestamp with time zone, $2 timestamp with time zone) returns tstzrange;
create FUNCTION tstzrange($1 timestamp with time zone, $2 timestamp with time zone, $3 text) returns tstzrange;
create FUNCTION tsvector_cmp($1 tsvector, $2 tsvector) returns integer;
create FUNCTION tsvector_concat($1 tsvector, $2 tsvector) returns tsvector;
create FUNCTION tsvector_eq($1 tsvector, $2 tsvector) returns boolean;
create FUNCTION tsvector_ge($1 tsvector, $2 tsvector) returns boolean;
create FUNCTION tsvector_gt($1 tsvector, $2 tsvector) returns boolean;
create FUNCTION tsvector_le($1 tsvector, $2 tsvector) returns boolean;
create FUNCTION tsvector_lt($1 tsvector, $2 tsvector) returns boolean;
create FUNCTION tsvector_ne($1 tsvector, $2 tsvector) returns boolean;
create FUNCTION tsvector_update_trigger();
create FUNCTION tsvector_update_trigger_column();
create FUNCTION tsvectorin($1 cstring) returns tsvector;
create FUNCTION tsvectorout($1 tsvector) returns cstring;
create FUNCTION tsvectorrecv($1 internal) returns tsvector;
create FUNCTION tsvectorsend($1 tsvector) returns bytea;
create FUNCTION txid_current();
create FUNCTION txid_current_snapshot();
create FUNCTION txid_snapshot_in($1 cstring) returns txid_snapshot;
create FUNCTION txid_snapshot_out($1 txid_snapshot) returns cstring;
create FUNCTION txid_snapshot_recv($1 internal) returns txid_snapshot;
create FUNCTION txid_snapshot_send($1 txid_snapshot) returns bytea;
create FUNCTION txid_snapshot_xip($1 txid_snapshot) returns setof bigint;
create FUNCTION txid_snapshot_xmax($1 txid_snapshot) returns bigint;
create FUNCTION txid_snapshot_xmin($1 txid_snapshot) returns bigint;
create FUNCTION txid_visible_in_snapshot($1 bigint, $2 txid_snapshot) returns boolean;
create FUNCTION uhc_to_utf8($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION unique_key_recheck();
create FUNCTION unknownin($1 cstring) returns unknown;
create FUNCTION unknownout($1 unknown) returns cstring;
create FUNCTION unknownrecv($1 internal) returns unknown;
create FUNCTION unknownsend($1 unknown) returns bytea;
create FUNCTION unnest($1 anyarray) returns setof anyelement;
create FUNCTION upper_inc($1 anyrange) returns boolean;
create FUNCTION upper_inf($1 anyrange) returns boolean;
create FUNCTION "upper"($1 anyrange) returns anyelement;
create FUNCTION "upper"($1 text) returns text;
create FUNCTION utf8_to_ascii($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION utf8_to_big5($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION utf8_to_euc_cn($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION utf8_to_euc_jis_2004($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION utf8_to_euc_jp($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION utf8_to_euc_kr($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION utf8_to_euc_tw($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION utf8_to_gb18030($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION utf8_to_gbk($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION utf8_to_iso8859_1($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION utf8_to_iso8859($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION utf8_to_johab($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION utf8_to_koi8r($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION utf8_to_koi8u($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION utf8_to_shift_jis_2004($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION utf8_to_sjis($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION utf8_to_uhc($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION utf8_to_win($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION uuid_cmp($1 uuid, $2 uuid) returns integer;
create FUNCTION uuid_eq($1 uuid, $2 uuid) returns boolean;
create FUNCTION uuid_ge($1 uuid, $2 uuid) returns boolean;
create FUNCTION uuid_gt($1 uuid, $2 uuid) returns boolean;
create FUNCTION uuid_hash($1 uuid) returns integer;
create FUNCTION uuid_in($1 cstring) returns uuid;
create FUNCTION uuid_le($1 uuid, $2 uuid) returns boolean;
create FUNCTION uuid_lt($1 uuid, $2 uuid) returns boolean;
create FUNCTION uuid_ne($1 uuid, $2 uuid) returns boolean;
create FUNCTION uuid_out($1 uuid) returns cstring;
create FUNCTION uuid_recv($1 internal) returns uuid;
create FUNCTION uuid_send($1 uuid) returns bytea;
create FUNCTION var_pop($1 bigint) returns numeric;
create FUNCTION var_pop($1 double precision) returns double precision;
create FUNCTION var_pop($1 integer) returns numeric;
create FUNCTION var_pop($1 numeric) returns numeric;
create FUNCTION var_pop($1 real) returns double precision;
create FUNCTION var_pop($1 smallint) returns numeric;
create FUNCTION var_samp($1 bigint) returns numeric;
create FUNCTION var_samp($1 double precision) returns double precision;
create FUNCTION var_samp($1 integer) returns numeric;
create FUNCTION var_samp($1 numeric) returns numeric;
create FUNCTION var_samp($1 real) returns double precision;
create FUNCTION var_samp($1 smallint) returns numeric;
create FUNCTION varbit_in($1 cstring, $2 oid, $3 integer) returns bit varying;
create FUNCTION varbit_out($1 bit varying) returns cstring;
create FUNCTION varbit_recv($1 internal, $2 oid, $3 integer) returns bit varying;
create FUNCTION varbit_send($1 bit varying) returns bytea;
create FUNCTION varbit_transform($1 internal) returns internal;
create FUNCTION varbit($1 bit varying, $2 integer, $3 boolean) returns bit varying;
create FUNCTION varbitcmp($1 bit varying, $2 bit varying) returns integer;
create FUNCTION varbiteq($1 bit varying, $2 bit varying) returns boolean;
create FUNCTION varbitge($1 bit varying, $2 bit varying) returns boolean;
create FUNCTION varbitgt($1 bit varying, $2 bit varying) returns boolean;
create FUNCTION varbitle($1 bit varying, $2 bit varying) returns boolean;
create FUNCTION varbitlt($1 bit varying, $2 bit varying) returns boolean;
create FUNCTION varbitne($1 bit varying, $2 bit varying) returns boolean;
create FUNCTION varbittypmodin($1 cstring[]) returns integer;
create FUNCTION varbittypmodout($1 integer) returns cstring;
create FUNCTION varchar_transform($1 internal) returns internal;
create FUNCTION varcharin($1 cstring, $2 oid, $3 integer) returns varchar;
create FUNCTION "varchar"($1 name) returns varchar;
create FUNCTION varcharout($1 varchar) returns cstring;
create FUNCTION varcharrecv($1 internal, $2 oid, $3 integer) returns varchar;
create FUNCTION varcharsend($1 varchar) returns bytea;
create FUNCTION varchartypmodin($1 cstring[]) returns integer;
create FUNCTION varchartypmodout($1 integer) returns cstring;
create FUNCTION "varchar"($1 varchar, $2 integer, $3 boolean) returns varchar;
create FUNCTION variance($1 bigint) returns numeric;
create FUNCTION variance($1 double precision) returns double precision;
create FUNCTION variance($1 integer) returns numeric;
create FUNCTION variance($1 numeric) returns numeric;
create FUNCTION variance($1 real) returns double precision;
create FUNCTION variance($1 smallint) returns numeric;
create FUNCTION version();
create FUNCTION void_in($1 cstring) returns void;
create FUNCTION void_out($1 void) returns cstring;
create FUNCTION void_recv($1 internal) returns void;
create FUNCTION void_send($1 void) returns bytea;
create FUNCTION width_bucket($1 double precision, $2 double precision, $3 double precision, $4 integer) returns integer;
create FUNCTION width_bucket($1 numeric, $2 numeric, $3 numeric, $4 integer) returns integer;
create FUNCTION width($1 box) returns double precision;
create FUNCTION win1250_to_latin2($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION win1250_to_mic($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION win1251_to_iso($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION win1251_to_koi8r($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION win1251_to_mic($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION win1251_to_win866($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION win866_to_iso($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION win866_to_koi8r($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION win866_to_mic($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION win866_to_win1251($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION win_to_utf8($1 integer, $2 integer, $3 cstring, $4 internal, $5 integer) returns void;
create FUNCTION xideqint4($1 xid, $2 integer) returns boolean;
create FUNCTION xideq($1 xid, $2 xid) returns boolean;
create FUNCTION xidin($1 cstring) returns xid;
create FUNCTION xidout($1 xid) returns cstring;
create FUNCTION xidrecv($1 internal) returns xid;
create FUNCTION xidsend($1 xid) returns bytea;
create FUNCTION xml_in($1 cstring) returns xml;
create FUNCTION xml_is_well_formed_content($1 text) returns boolean;
create FUNCTION xml_is_well_formed_document($1 text) returns boolean;
create FUNCTION xml_is_well_formed($1 text) returns boolean;
create FUNCTION xml_out($1 xml) returns cstring;
create FUNCTION xml_recv($1 internal) returns xml;
create FUNCTION xml_send($1 xml) returns bytea;
create FUNCTION xmlagg($1 xml) returns xml;
create FUNCTION xmlcomment($1 text) returns xml;
create FUNCTION xmlconcat2($1 xml, $2 xml) returns xml;
create FUNCTION xmlexists($1 text, $2 xml) returns boolean;
create FUNCTION xml($1 text) returns xml;
create FUNCTION xmlvalidate($1 xml, $2 text) returns boolean;
create FUNCTION xpath_exists($1 text, $2 xml) returns boolean;
create FUNCTION xpath_exists($1 text, $2 xml, $3 text[]) returns boolean;
create FUNCTION xpath($1 text, $2 xml) returns xml[];
create FUNCTION xpath($1 text, $2 xml, $3 text[]) returns xml[];
create TABLE sql_features (
    feature_id information_schema.char_data,
    feature_name information_schema.char_data,
    sub_feature_id information_schema.char_data,
    sub_feature_name information_schema.char_data,
    is_supported information_schema.yes_or_no,
    is_verified_by information_schema.char_data,
    comments information_schema.char_data
);
create TABLE sql_implementation_info (
    implementation_info_id information_schema.char_data,
    implementation_info_name information_schema.char_data,
    integer_value information_schema.cardinal_number,
    character_value information_schema.char_data,
    comments information_schema.char_data
);
create TABLE sql_languages (
    sql_language_source information_schema.char_data,
    sql_language_year information_schema.char_data,
    sql_language_conformance information_schema.char_data,
    sql_language_integrity information_schema.char_data,
    sql_language_implementation information_schema.char_data,
    sql_language_binding_style information_schema.char_data,
    sql_language_programming_language information_schema.char_data
);
create TABLE sql_packages (
    feature_id information_schema.char_data,
    feature_name information_schema.char_data,
    is_supported information_schema.yes_or_no,
    is_verified_by information_schema.char_data,
    comments information_schema.char_data
);
create TABLE sql_parts (
    feature_id information_schema.char_data,
    feature_name information_schema.char_data,
    is_supported information_schema.yes_or_no,
    is_verified_by information_schema.char_data,
    comments information_schema.char_data
);
create TABLE sql_sizing (
    sizing_id information_schema.cardinal_number,
    sizing_name information_schema.char_data,
    supported_value information_schema.cardinal_number,
    comments information_schema.char_data
);
create TABLE sql_sizing_profiles (
    sizing_id information_schema.cardinal_number,
    sizing_name information_schema.char_data,
    profile_id information_schema.char_data,
    required_value information_schema.cardinal_number,
    comments information_schema.char_data
);
create VIEW _pg_foreign_data_wrappers (
    oid oid,
    fdwowner oid,
    fdwoptions text[],
    foreign_data_wrapper_catalog information_schema.sql_identifier,
    foreign_data_wrapper_name information_schema.sql_identifier,
    authorization_identifier information_schema.sql_identifier,
    foreign_data_wrapper_language information_schema.char_data
);
create VIEW _pg_foreign_servers (
    oid oid,
    srvoptions text[],
    foreign_server_catalog information_schema.sql_identifier,
    foreign_server_name information_schema.sql_identifier,
    foreign_data_wrapper_catalog information_schema.sql_identifier,
    foreign_data_wrapper_name information_schema.sql_identifier,
    foreign_server_type information_schema.char_data,
    foreign_server_version information_schema.char_data,
    authorization_identifier information_schema.sql_identifier
);
create VIEW _pg_foreign_table_columns (
    nspname name,
    relname name,
    attname name,
    attfdwoptions text[]
);
create VIEW _pg_foreign_tables (
    foreign_table_catalog information_schema.sql_identifier,
    foreign_table_schema name,
    foreign_table_name name,
    ftoptions text[],
    foreign_server_catalog information_schema.sql_identifier,
    foreign_server_name information_schema.sql_identifier,
    authorization_identifier information_schema.sql_identifier
);
create VIEW _pg_user_mappings (
    oid oid,
    umoptions text[],
    umuser oid,
    authorization_identifier information_schema.sql_identifier,
    foreign_server_catalog information_schema.sql_identifier,
    foreign_server_name information_schema.sql_identifier,
    srvowner information_schema.sql_identifier
);
create VIEW administrable_role_authorizations (
    grantee information_schema.sql_identifier,
    role_name information_schema.sql_identifier,
    is_grantable information_schema.yes_or_no
);
create VIEW applicable_roles (
    grantee information_schema.sql_identifier,
    role_name information_schema.sql_identifier,
    is_grantable information_schema.yes_or_no
);
create VIEW attributes (
    udt_catalog information_schema.sql_identifier,
    udt_schema information_schema.sql_identifier,
    udt_name information_schema.sql_identifier,
    attribute_name information_schema.sql_identifier,
    ordinal_position information_schema.cardinal_number,
    attribute_default information_schema.char_data,
    is_nullable information_schema.yes_or_no,
    data_type information_schema.char_data,
    character_maximum_length information_schema.cardinal_number,
    character_octet_length information_schema.cardinal_number,
    character_set_catalog information_schema.sql_identifier,
    character_set_schema information_schema.sql_identifier,
    character_set_name information_schema.sql_identifier,
    collation_catalog information_schema.sql_identifier,
    collation_schema information_schema.sql_identifier,
    collation_name information_schema.sql_identifier,
    numeric_precision information_schema.cardinal_number,
    numeric_precision_radix information_schema.cardinal_number,
    numeric_scale information_schema.cardinal_number,
    datetime_precision information_schema.cardinal_number,
    interval_type information_schema.char_data,
    interval_precision information_schema.cardinal_number,
    attribute_udt_catalog information_schema.sql_identifier,
    attribute_udt_schema information_schema.sql_identifier,
    attribute_udt_name information_schema.sql_identifier,
    scope_catalog information_schema.sql_identifier,
    scope_schema information_schema.sql_identifier,
    scope_name information_schema.sql_identifier,
    maximum_cardinality information_schema.cardinal_number,
    dtd_identifier information_schema.sql_identifier,
    is_derived_reference_attribute information_schema.yes_or_no
);
create VIEW character_sets (
    character_set_catalog information_schema.sql_identifier,
    character_set_schema information_schema.sql_identifier,
    character_set_name information_schema.sql_identifier,
    character_repertoire information_schema.sql_identifier,
    form_of_use information_schema.sql_identifier,
    default_collate_catalog information_schema.sql_identifier,
    default_collate_schema information_schema.sql_identifier,
    default_collate_name information_schema.sql_identifier
);
create VIEW check_constraint_routine_usage (
    constraint_catalog information_schema.sql_identifier,
    constraint_schema information_schema.sql_identifier,
    constraint_name information_schema.sql_identifier,
    specific_catalog information_schema.sql_identifier,
    specific_schema information_schema.sql_identifier,
    specific_name information_schema.sql_identifier
);
create VIEW check_constraints (
    constraint_catalog information_schema.sql_identifier,
    constraint_schema information_schema.sql_identifier,
    constraint_name information_schema.sql_identifier,
    check_clause information_schema.char_data
);
create VIEW collation_character_set_applicability (
    collation_catalog information_schema.sql_identifier,
    collation_schema information_schema.sql_identifier,
    collation_name information_schema.sql_identifier,
    character_set_catalog information_schema.sql_identifier,
    character_set_schema information_schema.sql_identifier,
    character_set_name information_schema.sql_identifier
);
create VIEW collations (
    collation_catalog information_schema.sql_identifier,
    collation_schema information_schema.sql_identifier,
    collation_name information_schema.sql_identifier,
    pad_attribute information_schema.char_data
);
create VIEW column_domain_usage (
    domain_catalog information_schema.sql_identifier,
    domain_schema information_schema.sql_identifier,
    domain_name information_schema.sql_identifier,
    table_catalog information_schema.sql_identifier,
    table_schema information_schema.sql_identifier,
    table_name information_schema.sql_identifier,
    column_name information_schema.sql_identifier
);
create VIEW column_options (
    table_catalog information_schema.sql_identifier,
    table_schema name,
    table_name name,
    column_name name,
    option_name information_schema.sql_identifier,
    option_value information_schema.char_data
);
create VIEW column_privileges (
    grantor information_schema.sql_identifier,
    grantee information_schema.sql_identifier,
    table_catalog information_schema.sql_identifier,
    table_schema information_schema.sql_identifier,
    table_name information_schema.sql_identifier,
    column_name information_schema.sql_identifier,
    privilege_type information_schema.char_data,
    is_grantable information_schema.yes_or_no
);
create VIEW column_udt_usage (
    udt_catalog information_schema.sql_identifier,
    udt_schema information_schema.sql_identifier,
    udt_name information_schema.sql_identifier,
    table_catalog information_schema.sql_identifier,
    table_schema information_schema.sql_identifier,
    table_name information_schema.sql_identifier,
    column_name information_schema.sql_identifier
);
create VIEW columns (
    table_catalog information_schema.sql_identifier,
    table_schema information_schema.sql_identifier,
    table_name information_schema.sql_identifier,
    column_name information_schema.sql_identifier,
    ordinal_position information_schema.cardinal_number,
    column_default information_schema.char_data,
    is_nullable information_schema.yes_or_no,
    data_type information_schema.char_data,
    character_maximum_length information_schema.cardinal_number,
    character_octet_length information_schema.cardinal_number,
    numeric_precision information_schema.cardinal_number,
    numeric_precision_radix information_schema.cardinal_number,
    numeric_scale information_schema.cardinal_number,
    datetime_precision information_schema.cardinal_number,
    interval_type information_schema.char_data,
    interval_precision information_schema.cardinal_number,
    character_set_catalog information_schema.sql_identifier,
    character_set_schema information_schema.sql_identifier,
    character_set_name information_schema.sql_identifier,
    collation_catalog information_schema.sql_identifier,
    collation_schema information_schema.sql_identifier,
    collation_name information_schema.sql_identifier,
    domain_catalog information_schema.sql_identifier,
    domain_schema information_schema.sql_identifier,
    domain_name information_schema.sql_identifier,
    udt_catalog information_schema.sql_identifier,
    udt_schema information_schema.sql_identifier,
    udt_name information_schema.sql_identifier,
    scope_catalog information_schema.sql_identifier,
    scope_schema information_schema.sql_identifier,
    scope_name information_schema.sql_identifier,
    maximum_cardinality information_schema.cardinal_number,
    dtd_identifier information_schema.sql_identifier,
    is_self_referencing information_schema.yes_or_no,
    is_identity information_schema.yes_or_no,
    identity_generation information_schema.char_data,
    identity_start information_schema.char_data,
    identity_increment information_schema.char_data,
    identity_maximum information_schema.char_data,
    identity_minimum information_schema.char_data,
    identity_cycle information_schema.yes_or_no,
    is_generated information_schema.char_data,
    generation_expression information_schema.char_data,
    is_updatable information_schema.yes_or_no
);
create VIEW constraint_column_usage (
    table_catalog information_schema.sql_identifier,
    table_schema information_schema.sql_identifier,
    table_name information_schema.sql_identifier,
    column_name information_schema.sql_identifier,
    constraint_catalog information_schema.sql_identifier,
    constraint_schema information_schema.sql_identifier,
    constraint_name information_schema.sql_identifier
);
create VIEW constraint_table_usage (
    table_catalog information_schema.sql_identifier,
    table_schema information_schema.sql_identifier,
    table_name information_schema.sql_identifier,
    constraint_catalog information_schema.sql_identifier,
    constraint_schema information_schema.sql_identifier,
    constraint_name information_schema.sql_identifier
);
create VIEW data_type_privileges (
    object_catalog information_schema.sql_identifier,
    object_schema information_schema.sql_identifier,
    object_name information_schema.sql_identifier,
    object_type information_schema.char_data,
    dtd_identifier information_schema.sql_identifier
);
create VIEW domain_constraints (
    constraint_catalog information_schema.sql_identifier,
    constraint_schema information_schema.sql_identifier,
    constraint_name information_schema.sql_identifier,
    domain_catalog information_schema.sql_identifier,
    domain_schema information_schema.sql_identifier,
    domain_name information_schema.sql_identifier,
    is_deferrable information_schema.yes_or_no,
    initially_deferred information_schema.yes_or_no
);
create VIEW domain_udt_usage (
    udt_catalog information_schema.sql_identifier,
    udt_schema information_schema.sql_identifier,
    udt_name information_schema.sql_identifier,
    domain_catalog information_schema.sql_identifier,
    domain_schema information_schema.sql_identifier,
    domain_name information_schema.sql_identifier
);
create VIEW domains (
    domain_catalog information_schema.sql_identifier,
    domain_schema information_schema.sql_identifier,
    domain_name information_schema.sql_identifier,
    data_type information_schema.char_data,
    character_maximum_length information_schema.cardinal_number,
    character_octet_length information_schema.cardinal_number,
    character_set_catalog information_schema.sql_identifier,
    character_set_schema information_schema.sql_identifier,
    character_set_name information_schema.sql_identifier,
    collation_catalog information_schema.sql_identifier,
    collation_schema information_schema.sql_identifier,
    collation_name information_schema.sql_identifier,
    numeric_precision information_schema.cardinal_number,
    numeric_precision_radix information_schema.cardinal_number,
    numeric_scale information_schema.cardinal_number,
    datetime_precision information_schema.cardinal_number,
    interval_type information_schema.char_data,
    interval_precision information_schema.cardinal_number,
    domain_default information_schema.char_data,
    udt_catalog information_schema.sql_identifier,
    udt_schema information_schema.sql_identifier,
    udt_name information_schema.sql_identifier,
    scope_catalog information_schema.sql_identifier,
    scope_schema information_schema.sql_identifier,
    scope_name information_schema.sql_identifier,
    maximum_cardinality information_schema.cardinal_number,
    dtd_identifier information_schema.sql_identifier
);
create VIEW element_types (
    object_catalog information_schema.sql_identifier,
    object_schema information_schema.sql_identifier,
    object_name information_schema.sql_identifier,
    object_type information_schema.char_data,
    collection_type_identifier information_schema.sql_identifier,
    data_type information_schema.char_data,
    character_maximum_length information_schema.cardinal_number,
    character_octet_length information_schema.cardinal_number,
    character_set_catalog information_schema.sql_identifier,
    character_set_schema information_schema.sql_identifier,
    character_set_name information_schema.sql_identifier,
    collation_catalog information_schema.sql_identifier,
    collation_schema information_schema.sql_identifier,
    collation_name information_schema.sql_identifier,
    numeric_precision information_schema.cardinal_number,
    numeric_precision_radix information_schema.cardinal_number,
    numeric_scale information_schema.cardinal_number,
    datetime_precision information_schema.cardinal_number,
    interval_type information_schema.char_data,
    interval_precision information_schema.cardinal_number,
    domain_default information_schema.char_data,
    udt_catalog information_schema.sql_identifier,
    udt_schema information_schema.sql_identifier,
    udt_name information_schema.sql_identifier,
    scope_catalog information_schema.sql_identifier,
    scope_schema information_schema.sql_identifier,
    scope_name information_schema.sql_identifier,
    maximum_cardinality information_schema.cardinal_number,
    dtd_identifier information_schema.sql_identifier
);
create VIEW enabled_roles (
    role_name information_schema.sql_identifier
);
create VIEW foreign_data_wrapper_options (
    foreign_data_wrapper_catalog information_schema.sql_identifier,
    foreign_data_wrapper_name information_schema.sql_identifier,
    option_name information_schema.sql_identifier,
    option_value information_schema.char_data
);
create VIEW foreign_data_wrappers (
    foreign_data_wrapper_catalog information_schema.sql_identifier,
    foreign_data_wrapper_name information_schema.sql_identifier,
    authorization_identifier information_schema.sql_identifier,
    library_name information_schema.char_data,
    foreign_data_wrapper_language information_schema.char_data
);
create VIEW foreign_server_options (
    foreign_server_catalog information_schema.sql_identifier,
    foreign_server_name information_schema.sql_identifier,
    option_name information_schema.sql_identifier,
    option_value information_schema.char_data
);
create VIEW foreign_servers (
    foreign_server_catalog information_schema.sql_identifier,
    foreign_server_name information_schema.sql_identifier,
    foreign_data_wrapper_catalog information_schema.sql_identifier,
    foreign_data_wrapper_name information_schema.sql_identifier,
    foreign_server_type information_schema.char_data,
    foreign_server_version information_schema.char_data,
    authorization_identifier information_schema.sql_identifier
);
create VIEW foreign_table_options (
    foreign_table_catalog information_schema.sql_identifier,
    foreign_table_schema name,
    foreign_table_name name,
    option_name information_schema.sql_identifier,
    option_value information_schema.char_data
);
create VIEW foreign_tables (
    foreign_table_catalog information_schema.sql_identifier,
    foreign_table_schema name,
    foreign_table_name name,
    foreign_server_catalog information_schema.sql_identifier,
    foreign_server_name information_schema.sql_identifier
);
create VIEW information_schema_catalog_name (
    catalog_name information_schema.sql_identifier
);
create VIEW key_column_usage (
    constraint_catalog information_schema.sql_identifier,
    constraint_schema information_schema.sql_identifier,
    constraint_name information_schema.sql_identifier,
    table_catalog information_schema.sql_identifier,
    table_schema information_schema.sql_identifier,
    table_name information_schema.sql_identifier,
    column_name information_schema.sql_identifier,
    ordinal_position information_schema.cardinal_number,
    position_in_unique_constraint information_schema.cardinal_number
);
create VIEW parameters (
    specific_catalog information_schema.sql_identifier,
    specific_schema information_schema.sql_identifier,
    specific_name information_schema.sql_identifier,
    ordinal_position information_schema.cardinal_number,
    parameter_mode information_schema.char_data,
    is_result information_schema.yes_or_no,
    as_locator information_schema.yes_or_no,
    parameter_name information_schema.sql_identifier,
    data_type information_schema.char_data,
    character_maximum_length information_schema.cardinal_number,
    character_octet_length information_schema.cardinal_number,
    character_set_catalog information_schema.sql_identifier,
    character_set_schema information_schema.sql_identifier,
    character_set_name information_schema.sql_identifier,
    collation_catalog information_schema.sql_identifier,
    collation_schema information_schema.sql_identifier,
    collation_name information_schema.sql_identifier,
    numeric_precision information_schema.cardinal_number,
    numeric_precision_radix information_schema.cardinal_number,
    numeric_scale information_schema.cardinal_number,
    datetime_precision information_schema.cardinal_number,
    interval_type information_schema.char_data,
    interval_precision information_schema.cardinal_number,
    udt_catalog information_schema.sql_identifier,
    udt_schema information_schema.sql_identifier,
    udt_name information_schema.sql_identifier,
    scope_catalog information_schema.sql_identifier,
    scope_schema information_schema.sql_identifier,
    scope_name information_schema.sql_identifier,
    maximum_cardinality information_schema.cardinal_number,
    dtd_identifier information_schema.sql_identifier,
    parameter_default information_schema.char_data
);
create VIEW referential_constraints (
    constraint_catalog information_schema.sql_identifier,
    constraint_schema information_schema.sql_identifier,
    constraint_name information_schema.sql_identifier,
    unique_constraint_catalog information_schema.sql_identifier,
    unique_constraint_schema information_schema.sql_identifier,
    unique_constraint_name information_schema.sql_identifier,
    match_option information_schema.char_data,
    update_rule information_schema.char_data,
    delete_rule information_schema.char_data
);
create VIEW role_column_grants (
    grantor information_schema.sql_identifier,
    grantee information_schema.sql_identifier,
    table_catalog information_schema.sql_identifier,
    table_schema information_schema.sql_identifier,
    table_name information_schema.sql_identifier,
    column_name information_schema.sql_identifier,
    privilege_type information_schema.char_data,
    is_grantable information_schema.yes_or_no
);
create VIEW role_routine_grants (
    grantor information_schema.sql_identifier,
    grantee information_schema.sql_identifier,
    specific_catalog information_schema.sql_identifier,
    specific_schema information_schema.sql_identifier,
    specific_name information_schema.sql_identifier,
    routine_catalog information_schema.sql_identifier,
    routine_schema information_schema.sql_identifier,
    routine_name information_schema.sql_identifier,
    privilege_type information_schema.char_data,
    is_grantable information_schema.yes_or_no
);
create VIEW role_table_grants (
    grantor information_schema.sql_identifier,
    grantee information_schema.sql_identifier,
    table_catalog information_schema.sql_identifier,
    table_schema information_schema.sql_identifier,
    table_name information_schema.sql_identifier,
    privilege_type information_schema.char_data,
    is_grantable information_schema.yes_or_no,
    with_hierarchy information_schema.yes_or_no
);
create VIEW role_udt_grants (
    grantor information_schema.sql_identifier,
    grantee information_schema.sql_identifier,
    udt_catalog information_schema.sql_identifier,
    udt_schema information_schema.sql_identifier,
    udt_name information_schema.sql_identifier,
    privilege_type information_schema.char_data,
    is_grantable information_schema.yes_or_no
);
create VIEW role_usage_grants (
    grantor information_schema.sql_identifier,
    grantee information_schema.sql_identifier,
    object_catalog information_schema.sql_identifier,
    object_schema information_schema.sql_identifier,
    object_name information_schema.sql_identifier,
    object_type information_schema.char_data,
    privilege_type information_schema.char_data,
    is_grantable information_schema.yes_or_no
);
create VIEW routine_privileges (
    grantor information_schema.sql_identifier,
    grantee information_schema.sql_identifier,
    specific_catalog information_schema.sql_identifier,
    specific_schema information_schema.sql_identifier,
    specific_name information_schema.sql_identifier,
    routine_catalog information_schema.sql_identifier,
    routine_schema information_schema.sql_identifier,
    routine_name information_schema.sql_identifier,
    privilege_type information_schema.char_data,
    is_grantable information_schema.yes_or_no
);
create VIEW routines (
    specific_catalog information_schema.sql_identifier,
    specific_schema information_schema.sql_identifier,
    specific_name information_schema.sql_identifier,
    routine_catalog information_schema.sql_identifier,
    routine_schema information_schema.sql_identifier,
    routine_name information_schema.sql_identifier,
    routine_type information_schema.char_data,
    module_catalog information_schema.sql_identifier,
    module_schema information_schema.sql_identifier,
    module_name information_schema.sql_identifier,
    udt_catalog information_schema.sql_identifier,
    udt_schema information_schema.sql_identifier,
    udt_name information_schema.sql_identifier,
    data_type information_schema.char_data,
    character_maximum_length information_schema.cardinal_number,
    character_octet_length information_schema.cardinal_number,
    character_set_catalog information_schema.sql_identifier,
    character_set_schema information_schema.sql_identifier,
    character_set_name information_schema.sql_identifier,
    collation_catalog information_schema.sql_identifier,
    collation_schema information_schema.sql_identifier,
    collation_name information_schema.sql_identifier,
    numeric_precision information_schema.cardinal_number,
    numeric_precision_radix information_schema.cardinal_number,
    numeric_scale information_schema.cardinal_number,
    datetime_precision information_schema.cardinal_number,
    interval_type information_schema.char_data,
    interval_precision information_schema.cardinal_number,
    type_udt_catalog information_schema.sql_identifier,
    type_udt_schema information_schema.sql_identifier,
    type_udt_name information_schema.sql_identifier,
    scope_catalog information_schema.sql_identifier,
    scope_schema information_schema.sql_identifier,
    scope_name information_schema.sql_identifier,
    maximum_cardinality information_schema.cardinal_number,
    dtd_identifier information_schema.sql_identifier,
    routine_body information_schema.char_data,
    routine_definition information_schema.char_data,
    external_name information_schema.char_data,
    external_language information_schema.char_data,
    parameter_style information_schema.char_data,
    is_deterministic information_schema.yes_or_no,
    sql_data_access information_schema.char_data,
    is_null_call information_schema.yes_or_no,
    sql_path information_schema.char_data,
    schema_level_routine information_schema.yes_or_no,
    max_dynamic_result_sets information_schema.cardinal_number,
    is_user_defined_cast information_schema.yes_or_no,
    is_implicitly_invocable information_schema.yes_or_no,
    security_type information_schema.char_data,
    to_sql_specific_catalog information_schema.sql_identifier,
    to_sql_specific_schema information_schema.sql_identifier,
    to_sql_specific_name information_schema.sql_identifier,
    as_locator information_schema.yes_or_no,
    created information_schema.time_stamp,
    last_altered information_schema.time_stamp,
    new_savepoint_level information_schema.yes_or_no,
    is_udt_dependent information_schema.yes_or_no,
    result_cast_from_data_type information_schema.char_data,
    result_cast_as_locator information_schema.yes_or_no,
    result_cast_char_max_length information_schema.cardinal_number,
    result_cast_char_octet_length information_schema.cardinal_number,
    result_cast_char_set_catalog information_schema.sql_identifier,
    result_cast_char_set_schema information_schema.sql_identifier,
    result_cast_character_set_name information_schema.sql_identifier,
    result_cast_collation_catalog information_schema.sql_identifier,
    result_cast_collation_schema information_schema.sql_identifier,
    result_cast_collation_name information_schema.sql_identifier,
    result_cast_numeric_precision information_schema.cardinal_number,
    result_cast_numeric_precision_radix information_schema.cardinal_number,
    result_cast_numeric_scale information_schema.cardinal_number,
    result_cast_datetime_precision information_schema.cardinal_number,
    result_cast_interval_type information_schema.char_data,
    result_cast_interval_precision information_schema.cardinal_number,
    result_cast_type_udt_catalog information_schema.sql_identifier,
    result_cast_type_udt_schema information_schema.sql_identifier,
    result_cast_type_udt_name information_schema.sql_identifier,
    result_cast_scope_catalog information_schema.sql_identifier,
    result_cast_scope_schema information_schema.sql_identifier,
    result_cast_scope_name information_schema.sql_identifier,
    result_cast_maximum_cardinality information_schema.cardinal_number,
    result_cast_dtd_identifier information_schema.sql_identifier
);
create VIEW schemata (
    catalog_name information_schema.sql_identifier,
    schema_name information_schema.sql_identifier,
    schema_owner information_schema.sql_identifier,
    default_character_set_catalog information_schema.sql_identifier,
    default_character_set_schema information_schema.sql_identifier,
    default_character_set_name information_schema.sql_identifier,
    sql_path information_schema.char_data
);
create VIEW sequences (
    sequence_catalog information_schema.sql_identifier,
    sequence_schema information_schema.sql_identifier,
    sequence_name information_schema.sql_identifier,
    data_type information_schema.char_data,
    numeric_precision information_schema.cardinal_number,
    numeric_precision_radix information_schema.cardinal_number,
    numeric_scale information_schema.cardinal_number,
    start_value information_schema.char_data,
    minimum_value information_schema.char_data,
    maximum_value information_schema.char_data,
    increment information_schema.char_data,
    cycle_option information_schema.yes_or_no
);
create VIEW table_constraints (
    constraint_catalog information_schema.sql_identifier,
    constraint_schema information_schema.sql_identifier,
    constraint_name information_schema.sql_identifier,
    table_catalog information_schema.sql_identifier,
    table_schema information_schema.sql_identifier,
    table_name information_schema.sql_identifier,
    constraint_type information_schema.char_data,
    is_deferrable information_schema.yes_or_no,
    initially_deferred information_schema.yes_or_no
);
create VIEW table_privileges (
    grantor information_schema.sql_identifier,
    grantee information_schema.sql_identifier,
    table_catalog information_schema.sql_identifier,
    table_schema information_schema.sql_identifier,
    table_name information_schema.sql_identifier,
    privilege_type information_schema.char_data,
    is_grantable information_schema.yes_or_no,
    with_hierarchy information_schema.yes_or_no
);
create VIEW tables (
    table_catalog information_schema.sql_identifier,
    table_schema information_schema.sql_identifier,
    table_name information_schema.sql_identifier,
    table_type information_schema.char_data,
    self_referencing_column_name information_schema.sql_identifier,
    reference_generation information_schema.char_data,
    user_defined_type_catalog information_schema.sql_identifier,
    user_defined_type_schema information_schema.sql_identifier,
    user_defined_type_name information_schema.sql_identifier,
    is_insertable_into information_schema.yes_or_no,
    is_typed information_schema.yes_or_no,
    commit_action information_schema.char_data
);
create VIEW triggered_update_columns (
    trigger_catalog information_schema.sql_identifier,
    trigger_schema information_schema.sql_identifier,
    trigger_name information_schema.sql_identifier,
    event_object_catalog information_schema.sql_identifier,
    event_object_schema information_schema.sql_identifier,
    event_object_table information_schema.sql_identifier,
    event_object_column information_schema.sql_identifier
);
create VIEW triggers (
    trigger_catalog information_schema.sql_identifier,
    trigger_schema information_schema.sql_identifier,
    trigger_name information_schema.sql_identifier,
    event_manipulation information_schema.char_data,
    event_object_catalog information_schema.sql_identifier,
    event_object_schema information_schema.sql_identifier,
    event_object_table information_schema.sql_identifier,
    action_order information_schema.cardinal_number,
    action_condition information_schema.char_data,
    action_statement information_schema.char_data,
    action_orientation information_schema.char_data,
    action_timing information_schema.char_data,
    action_reference_old_table information_schema.sql_identifier,
    action_reference_new_table information_schema.sql_identifier,
    action_reference_old_row information_schema.sql_identifier,
    action_reference_new_row information_schema.sql_identifier,
    created information_schema.time_stamp
);
create VIEW udt_privileges (
    grantor information_schema.sql_identifier,
    grantee information_schema.sql_identifier,
    udt_catalog information_schema.sql_identifier,
    udt_schema information_schema.sql_identifier,
    udt_name information_schema.sql_identifier,
    privilege_type information_schema.char_data,
    is_grantable information_schema.yes_or_no
);
create VIEW usage_privileges (
    grantor information_schema.sql_identifier,
    grantee information_schema.sql_identifier,
    object_catalog information_schema.sql_identifier,
    object_schema information_schema.sql_identifier,
    object_name information_schema.sql_identifier,
    object_type information_schema.char_data,
    privilege_type information_schema.char_data,
    is_grantable information_schema.yes_or_no
);
create VIEW user_defined_types (
    user_defined_type_catalog information_schema.sql_identifier,
    user_defined_type_schema information_schema.sql_identifier,
    user_defined_type_name information_schema.sql_identifier,
    user_defined_type_category information_schema.char_data,
    is_instantiable information_schema.yes_or_no,
    is_final information_schema.yes_or_no,
    ordering_form information_schema.char_data,
    ordering_category information_schema.char_data,
    ordering_routine_catalog information_schema.sql_identifier,
    ordering_routine_schema information_schema.sql_identifier,
    ordering_routine_name information_schema.sql_identifier,
    reference_type information_schema.char_data,
    data_type information_schema.char_data,
    character_maximum_length information_schema.cardinal_number,
    character_octet_length information_schema.cardinal_number,
    character_set_catalog information_schema.sql_identifier,
    character_set_schema information_schema.sql_identifier,
    character_set_name information_schema.sql_identifier,
    collation_catalog information_schema.sql_identifier,
    collation_schema information_schema.sql_identifier,
    collation_name information_schema.sql_identifier,
    numeric_precision information_schema.cardinal_number,
    numeric_precision_radix information_schema.cardinal_number,
    numeric_scale information_schema.cardinal_number,
    datetime_precision information_schema.cardinal_number,
    interval_type information_schema.char_data,
    interval_precision information_schema.cardinal_number,
    source_dtd_identifier information_schema.sql_identifier,
    ref_dtd_identifier information_schema.sql_identifier
);
create VIEW user_mapping_options (
    authorization_identifier information_schema.sql_identifier,
    foreign_server_catalog information_schema.sql_identifier,
    foreign_server_name information_schema.sql_identifier,
    option_name information_schema.sql_identifier,
    option_value information_schema.char_data
);
create VIEW user_mappings (
    authorization_identifier information_schema.sql_identifier,
    foreign_server_catalog information_schema.sql_identifier,
    foreign_server_name information_schema.sql_identifier
);
create VIEW view_column_usage (
    view_catalog information_schema.sql_identifier,
    view_schema information_schema.sql_identifier,
    view_name information_schema.sql_identifier,
    table_catalog information_schema.sql_identifier,
    table_schema information_schema.sql_identifier,
    table_name information_schema.sql_identifier,
    column_name information_schema.sql_identifier
);
create VIEW view_routine_usage (
    table_catalog information_schema.sql_identifier,
    table_schema information_schema.sql_identifier,
    table_name information_schema.sql_identifier,
    specific_catalog information_schema.sql_identifier,
    specific_schema information_schema.sql_identifier,
    specific_name information_schema.sql_identifier
);
create VIEW view_table_usage (
    view_catalog information_schema.sql_identifier,
    view_schema information_schema.sql_identifier,
    view_name information_schema.sql_identifier,
    table_catalog information_schema.sql_identifier,
    table_schema information_schema.sql_identifier,
    table_name information_schema.sql_identifier
);
create VIEW views (
    table_catalog information_schema.sql_identifier,
    table_schema information_schema.sql_identifier,
    table_name information_schema.sql_identifier,
    view_definition information_schema.char_data,
    check_option information_schema.char_data,
    is_updatable information_schema.yes_or_no,
    is_insertable_into information_schema.yes_or_no,
    is_trigger_updatable information_schema.yes_or_no,
    is_trigger_deletable information_schema.yes_or_no,
    is_trigger_insertable_into information_schema.yes_or_no
);
create FUNCTION _pg_char_max_length(typid oid, typmod integer) returns integer;
create FUNCTION _pg_char_octet_length(typid oid, typmod integer) returns integer;
create FUNCTION _pg_datetime_precision(typid oid, typmod integer) returns integer;
create FUNCTION _pg_expandarray(anyarray, x anyelement, n integer) returns setof record;
create FUNCTION _pg_index_position($1 oid, $2 smallint) returns integer;
create FUNCTION _pg_interval_type(typid oid, mod integer) returns text;
create FUNCTION _pg_keysequal($1 smallint[], $2 smallint[]) returns boolean;
create FUNCTION _pg_numeric_precision_radix(typid oid, typmod integer) returns integer;
create FUNCTION _pg_numeric_precision(typid oid, typmod integer) returns integer;
create FUNCTION _pg_numeric_scale(typid oid, typmod integer) returns integer;
create FUNCTION _pg_truetypid($1 pg_attribute, $2 pg_type) returns oid;
create FUNCTION _pg_truetypmod($1 pg_attribute, $2 pg_type) returns integer;